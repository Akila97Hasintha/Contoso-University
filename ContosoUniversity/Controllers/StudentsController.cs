using AutoMapper;
using ContosoUniversity.Comon.Interfaces;
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
    public class StudentsController : Controller
    {
       
        private IStudentServices studentServices;
        private  Mapping _map;
       

        public StudentsController(IStudentServices studentServices,Mapping map)
        {
            
            this.studentServices = studentServices;
            _map = map;

            
        }
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            var studentEntity = await studentServices.GetStudents();
            var student = _map.studentsToListStudent(studentEntity);
            
            
            return View(student);
        }
        // Get: Students/Details
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);

            if (studentEntity == null)
            {
                return NotFound();
            }


            var studentModel = _map.studentToStudentModel(studentEntity);
            return View(studentModel);
        }
        // Get: Students/Create
        public IActionResult create()
        {
            return View();
        }
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentModel studentmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentEntity = _map.studentModelToStudent(studentmodel);
                    await studentServices.CreateStudent(studentEntity);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save ");
            }

            return View(studentmodel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);
            if (studentEntity == null)
            {
                return NotFound();
            }
            var studentModel = _map.studentToStudentModel(studentEntity);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(studentModel);
        }

        // post:student/delete

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await studentServices.DeleteStudent(id);
            
            return RedirectToAction(nameof(Index));
        }

        // get: Students/Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);

            if (studentEntity == null)
            {
                return NotFound();
            }

            var studentModel = _map.studentToStudentModel(studentEntity);

            return View(studentModel);
        }


        //Post : Student/edit
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id,StudentModel studentModel)
        {
            if(id == null)
            {
                return NotFound();
            }

            var studentEntity = _map.studentModelToStudent(studentModel);


            if (ModelState.IsValid)
            {
                
                try
                {
                    await studentServices.UpdateStudent(studentEntity);
                    return base.RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save ");
                }

            }
            return View(studentModel);
        }




    }
}
