using AutoMapper;
using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Comon.Repositories;
using ContosoUniversity.Comon.Servises;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using ContosoUniversity.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
       
        private ICourseServices courseServices;
        private Mapping _map;

        public CoursesController(ICourseServices courseServices,Mapping map)
        {
            this.courseServices = courseServices;
            _map = map;
            
        }
        //get : Courses
        public  async Task<IActionResult> Index()
        {
            var CourseEntities = await courseServices.GetAllCourses();
            var courses = _map.coursessToListCourses(CourseEntities);
;            return View(courses);
        }

        // get Deatils
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);

            if (courseEntity == null)
            {
                return NotFound();
            }


            var courseModel = _map.courseToCourseModel(courseEntity);
            return View(courseModel);
        }
        // get : Courses/Create
        public IActionResult create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Credits")] CourseModel courseModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var courseEntity = _map.courseModelToCourse(courseModel);
                    await courseServices.CreateCourse(courseEntity);
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save ");
            }

            return View(courseModel);
        }


        //  get Course/Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);
            if (courseEntity == null)
            {
                return NotFound();
            }
            var courseModel = _map.courseToCourseModel(courseEntity);



            return View(courseModel);
        }
        // post : Course/edit
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id,CourseModel courseModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = _map.courseModelToCourse(courseModel);
            if (ModelState.IsValid)
            {
                try
                {
                    await courseServices.UpdateStudent(courseEntity);
                    return base.RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save ");
                }

            }
            return View(courseModel);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);

            if (courseEntity == null)
            {
                return NotFound();
            }

            var courseModel = _map.courseToCourseModel(courseEntity);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(courseModel);
        }

        // post:Courses/delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await courseServices.DeletCourse(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
