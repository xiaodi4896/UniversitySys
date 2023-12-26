using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dxxt.Data;
using dxxt.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using dxxt.Service;

namespace dxxt.Controllers
{
    public class ScoresController : Controller
    {
        private readonly IScoreSERV _Scoreserv;
        private readonly IStudentSERV _Studentserv;
        private readonly ISubjectSERV _Subjectserv;

        public ScoresController(IScoreSERV Scoreserv,IStudentSERV Studentserv, ISubjectSERV Subjectserv)
        {
            _Scoreserv = Scoreserv;
            _Studentserv = Studentserv;
            _Subjectserv = Subjectserv;
        }

        // GET: Scores
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchQuery = "")
        {
            var (scores, totalCount) = await _Scoreserv.GetScoreListAsync(pageNumber, pageSize, searchQuery);
            var viewModel = new ScoreIndexViewModel
            {
                Scores = scores,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
            return View(viewModel);
        }


        // GET: Scores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var scoreStuSub = (await _Scoreserv.GetScoreByIdAsync(id)).FirstOrDefault();
            if (scoreStuSub == null)
            {
                return NotFound();
            }

            return View(scoreStuSub);
        }

        // GET: Scores/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var (students, _) = await _Studentserv.GetStudentListAsync(1, int.MaxValue, "");
            ViewBag.Students = new SelectList(students, "StudentID", "StudentName");

            // Initially populate with all subjects
            var (subjects, _) = await _Subjectserv.GetSubjectListAsync(1, int.MaxValue, "");
            ViewBag.Subjects = new SelectList(subjects, "SubjectID", "SubjectName");

            return View(new ScoreStuSub());
        }




        // POST: Scores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScoreID,StudentID,SubjectID,Marks,IsDelete")] Score score)
        {
            if (score.Marks.HasValue && (score.Marks < 0 || score.Marks > 100))
            {
                ModelState.AddModelError("Marks", "Marks must be between 0 and 100.");
            }

            if (await _Scoreserv.DoesScoreExist(score.StudentID, score.SubjectID))
            {
                ModelState.AddModelError("", "This student is already enrolled in this subject.");
            }

            if (!await _Scoreserv.CanEnrollInSubject(score.StudentID))
            {
                ModelState.AddModelError("", "Student cannot enroll in more than 10 failed or pending subjects.");
            }

            if (ModelState.IsValid)
            {
                await _Scoreserv.AddScoreAsync(score);
                return RedirectToAction(nameof(Index));
            }
            await PopulateStudentsAndSubjects();
            return View(new ScoreStuSub { });
        }

        // GET: Scores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scoreStuSub = (await _Scoreserv.GetScoreByIdAsync(id)).FirstOrDefault();
            if (scoreStuSub == null)
            {
                return NotFound();
            }


            await PopulateStudentsAndSubjects();
            return View(scoreStuSub);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScoreID,StudentID,SubjectID,Marks,IsDelete")] Score score)
        {
            if (id != score.ScoreID)
            {
                return NotFound();
            }

            if (score.Marks.HasValue && (score.Marks < 0 || score.Marks > 100))
            {
                ModelState.AddModelError("Marks", "Marks must be between 0 and 100.");
            }

            if (await _Scoreserv.DoesScoreExist(score.StudentID, score.SubjectID, score.ScoreID))
            {
                ModelState.AddModelError("", "This student is already enrolled in this subject.");
            }

            if (!await _Scoreserv.CanEnrollInSubject(score.StudentID, score.ScoreID))
            {
                ModelState.AddModelError("", "Student cannot enroll in more than 10 failed or pending subjects.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _Scoreserv.UpdateScoreAsync(score);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await _Scoreserv.GetScoreByIdAsync(id)).Any())
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Fetch the existing score details including StudentName and SubjectName
            var scoreStuSub = (await _Scoreserv.GetScoreByIdAsync(id)).FirstOrDefault();
            if (scoreStuSub == null)
            {
                return NotFound();
            }

            // Update the Marks with the value from the submitted form
            scoreStuSub.Marks = score.Marks;
            return View(scoreStuSub);
        }


        // GET: Scores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var scoreStuSub = (await _Scoreserv.GetScoreByIdAsync(id.Value)).FirstOrDefault();
            if (scoreStuSub == null)
            {
                return NotFound();
            }

            return View(scoreStuSub);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _Scoreserv.DeleteScoreAsync(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task<int> PopulateStudentsAndSubjects()
        {
            var (students, _) = await _Studentserv.GetStudentListAsync(1, int.MaxValue, null);
            ViewBag.Students = new SelectList(students, "StudentID", "StudentName");
            var (subjects, _) = await _Subjectserv.GetSubjectListAsync(1, int.MaxValue, null);
            ViewBag.Subjects = new SelectList(subjects, "SubjectID", "SubjectName");
            return 0;
        }
        public async Task<IActionResult> GroupedScores()
        {
            var groupedScores = await _Scoreserv.GetStudentGroupedScoresAsync();
            return View(groupedScores);
        }
        [HttpGet]
        public async Task<JsonResult> GetAvailableSubjects(int studentId)
        {
            var availableSubjects = await _Subjectserv.GetAvailableSubjectsForStudentAsync(studentId);
            return Json(availableSubjects);
        }
    }
}
