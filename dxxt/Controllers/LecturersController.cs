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
using System.Drawing.Printing;
namespace dxxt.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ILecturerSERV _Lecturerserv;

        public LecturersController(ILecturerSERV Lecturerserv)
        {
            _Lecturerserv = Lecturerserv;
        }

        // GET: Lecturers
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchQuery = null)
        {
            var (lecturers, totalCount) = await _Lecturerserv.GetLecturerListAsync(pageNumber, pageSize, searchQuery);

            var model = new LecturerIndexViewModel
            {
                Lecturers = lecturers,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

            return View(model);
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var lecturers = await _Lecturerserv.GetLecturerByIdAsync(id);
            if (lecturers == null)
            {
                return NotFound();
            }

            var subjects = await _Lecturerserv.GetSubjectsByLecturerIdAsync(id);
            var lecturer = lecturers.FirstOrDefault();

            lecturer.Subjects = subjects.ToList();
            return View(lecturer);
        }
        public IActionResult Create()
        {         
            return View();
        }

        // POST: Lecturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LecturerID,LecturerName,IsDelete")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                await _Lecturerserv.AddLecturerAsync(lecturer);
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }


        // GET: Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var lecturer = await _Lecturerserv.GetLecturerByIdAsync(id.Value);
            if (lecturer == null)
            {
                return NotFound();
            }
            return View(lecturer.FirstOrDefault());
        }

        // POST: Lecturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LecturerID,LecturerName,IsDelete")] Lecturer lecturer)
        {
            if (id != lecturer.LecturerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _Lecturerserv.UpdateLecturerAsync(lecturer);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    var exists = await _Lecturerserv.GetLecturerByIdAsync(lecturer.LecturerID) != null;
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
            return View(lecturer);
        }



        // GET: Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var lecturer = await _Lecturerserv.GetLecturerByIdAsync(id.Value);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer.FirstOrDefault());
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _Lecturerserv.IsLecturerTeachingSubjectsAsync(id))
            {
                // Handle the case when deletion is not allowed
                // Redirect to an error page or show a message
                return RedirectToAction(nameof(Index)); // Modify as needed
            }

            await _Lecturerserv.DeleteLecturerAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IsTeachingSubjects(int id)
        {
            var isTeaching = await _Lecturerserv.IsLecturerTeachingSubjectsAsync(id);
            return Json(isTeaching);
        }
    }
}