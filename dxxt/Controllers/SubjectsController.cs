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
    public class SubjectsController : Controller
    {
        private readonly ISubjectSERV _Subjectserv;
        private readonly ILecturerSERV _Lecturerserv;

        public SubjectsController(ISubjectSERV Subjectserv, ILecturerSERV Lecturerserv)
        {
            _Subjectserv = Subjectserv;
            _Lecturerserv = Lecturerserv;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchQuery = "")
        {
            var (subjects, totalCount) = await _Subjectserv.GetSubjectListAsync(pageNumber, pageSize, searchQuery);
            var viewModel = new SubjectIndexViewModel
            {
                Subjects = subjects,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            return View(viewModel);
        }
        // GET: Subjects
        public async Task<IActionResult> Details(int id)
        {
            var subjectLec = (await _Subjectserv.GetSubjectByIdAsync(id)).FirstOrDefault();
            if (subjectLec == null)
            {
                return NotFound();
            }

            var subject = new Subject
            {
                SubjectID = subjectLec.SubjectID,
                SubjectName = subjectLec.SubjectName,
                LecturerID = subjectLec.LecturerID,
                IsDelete = subjectLec.IsDelete
            };

            return View(subject);
        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            var result = await PopulateLecturers();
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectID,SubjectName,LecturerID,IsDelete")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (await _Subjectserv.SubjectNameExistsAsync(subject.SubjectName))
                {
                    ModelState.AddModelError("SubjectName", "Subject name already exists.");
                    await PopulateLecturers();
                    return View(subject);
                }
                await _Subjectserv.AddSubjectAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            await PopulateLecturers();
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            // Getting the SubjectLec object
            var subjectLec = (await _Subjectserv.GetSubjectByIdAsync(id.Value)).FirstOrDefault();
            if (subjectLec == null)
            {
                return NotFound();
            }

            // Mapping SubjectLec to Subject
            var subject = new Subject
            {
                SubjectID = subjectLec.SubjectID,
                SubjectName = subjectLec.SubjectName,
                LecturerID = subjectLec.LecturerID,
                IsDelete = subjectLec.IsDelete
            };

            await PopulateLecturers();
            return View(subject);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectID,SubjectName,LecturerID,IsDelete")] Subject subject)
        {
            if (id != subject.SubjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingSubject = (await _Subjectserv.GetSubjectByIdAsync(subject.SubjectID)).FirstOrDefault();
                if (existingSubject != null &&
                    existingSubject.SubjectName != subject.SubjectName &&
                    await _Subjectserv.SubjectNameExistsAsync(subject.SubjectName))
                {
                    ModelState.AddModelError("SubjectName", "Subject name already exists.");
                    await PopulateLecturers();
                    return View(subject);
                }

                try
                {
                    await _Subjectserv.UpdateSubjectAsync(subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SubjectExists(subject.SubjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            await PopulateLecturers();
            return View(subject);
        }

        private async Task<bool> SubjectExists(int id)
        {
            return (await _Subjectserv.GetSubjectByIdAsync(id)).Any();
        }


        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var subjectLec = (await _Subjectserv.GetSubjectByIdAsync(id.Value)).FirstOrDefault();
            if (subjectLec == null)
            {
                return NotFound();
            }

            var subject = new Subject
            {
                SubjectID = subjectLec.SubjectID,
                SubjectName = subjectLec.SubjectName,
                LecturerID = subjectLec.LecturerID,
                IsDelete = subjectLec.IsDelete
            };
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Subjectserv.DeleteSubjectAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<int> PopulateLecturers()
        {
            var (lecturers, _) = await _Lecturerserv.GetLecturerListAsync(1, int.MaxValue, null);
            ViewBag.Lecturers = new SelectList(lecturers, "LecturerID", "LecturerName");
            return 0;
        }


    }
}
