using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dxxt.Data;
using dxxt.Models;
using dxxt.Service;

namespace dxxt.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentSERV _Studentserv;

        public StudentsController(IStudentSERV Studentserv)
        {
            _Studentserv = Studentserv;
        }

        // GET: Students
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchQuery = "")
        {
            var (students, totalCount) = await _Studentserv.GetStudentListAsync(pageNumber, pageSize, searchQuery);
            var model = new StudentIndexViewModel
            {
                Students = students,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            return View(model);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var studentDetailsViewModel = await _Studentserv.GetStudentDetailsAsync(id);
            if (studentDetailsViewModel == null)
            {
                return NotFound();
            }

            // Set CanEnrollMore here, ensure it's never null
            ViewBag.CanEnrollMore = studentDetailsViewModel.CanEnrollMore; // or a default value

            return View(studentDetailsViewModel);
        }


        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,StudentName,Email,Intake,IsDelete")] Student student)
        {
            if (!IsValidEmail(student.Email))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
            }
            else if (await _Studentserv.StudentEmailExistsAsync(student.Email))
            {
                ModelState.AddModelError("Email", "Email already in use.");
            }

            if (HasInvalidCharacters(student.StudentName))
            {
                ModelState.AddModelError("StudentName", "Name contains invalid characters.");
            }

            if (student.Intake > DateTime.Now)
            {
                ModelState.AddModelError("Intake", "Intake date cannot be in the future.");
            }

            if (ModelState.IsValid)
            {
                await _Studentserv.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }



        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var student = await _Studentserv.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student.FirstOrDefault());
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,StudentName,Email,Intake,IsDelete")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }
            var existingStudents = await _Studentserv.GetStudentByIdAsync(id);
            var existingStudent = existingStudents.FirstOrDefault();

            if (existingStudent == null)
            {
                return NotFound();
            }

            // Validate email format and uniqueness
            if (!IsValidEmail(student.Email))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
            }
            else if (existingStudent.Email != student.Email && await _Studentserv.StudentEmailExistsAsync(student.Email))
            {
                ModelState.AddModelError("Email", "Email already in use.");
            }

            // Validate student name for invalid characters
            if (HasInvalidCharacters(student.StudentName))
            {
                ModelState.AddModelError("StudentName", "Name contains invalid characters.");
            }

            if (student.Intake > DateTime.Now)
            {
                ModelState.AddModelError("Intake", "Intake date cannot be in the future.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _Studentserv.UpdateStudentAsync(student);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _Studentserv.GetStudentByIdAsync(id) != null;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var student = await _Studentserv.GetStudentByIdAsync(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student.FirstOrDefault());
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Studentserv.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool HasInvalidCharacters(string name)
        {
            return name.Any(ch => !char.IsLetter(ch) && !char.IsWhiteSpace(ch));
        }

    }
}
