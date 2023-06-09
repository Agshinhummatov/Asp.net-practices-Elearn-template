﻿using Elearn_temp.Data;
using Elearn_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elearn_temp.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class CourseController : Controller
    {



        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Include(m => m.CourseImages).Include(m => m.Author).Where(m => !m.SoftDelete).ToListAsync();

            return View(courses);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();


            Course? courses = await _context.Courses.Include(m => m.CourseImages).Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);


            if (courses is null) return NotFound();

            return View(courses);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



    }
}
