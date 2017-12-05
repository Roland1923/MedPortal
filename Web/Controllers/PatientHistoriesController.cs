using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.IRepositories;
using Web.Models;

namespace Web.Controllers
{
    public class PatientHistoriesController : Controller
    {
        private readonly IPatientHistoryRepository _repository;

        public PatientHistoriesController(IPatientHistoryRepository repository)
        {
            _repository = repository;
        }

        // GET: PatientHistories
        public IActionResult Index()
        {
            return View(_repository.GetAllPatientHistories());
        }

        // GET: PatientHistories/Details/5
        public IActionResult Details(Guid id)
        {
            var patientHistory = _repository.GetPatientHistoryById(id);

            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }

        // GET: PatientHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Patient, Doctor, Prescription, Description, Recomandations")] PatientHistoryModel patientHistory)
        {
            var instance = PatientHistory.Create(patientHistory.Patient, patientHistory.Doctor, patientHistory.Prescription, patientHistory.Description, patientHistory.Recomandations);
            if (ModelState.IsValid)
            {
                _repository.AddPatientHistory(instance);

                return RedirectToAction(nameof(Index));
            }

            return View(instance);
        }

        // GET: PatientHistories/Edit/5
        public IActionResult Edit(Guid id)
        {
            var patientHistory = _repository.GetPatientHistoryById(id);

            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }

        // POST: PatientHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Patient, Doctor, Prescription, Description, Recomandations")] PatientHistoryModel patientHistory)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var entity = _repository.GetPatientHistoryById(id);

            try
            {
                if (ModelState.IsValid)
                {
                    entity.Update(patientHistory.Patient, patientHistory.Doctor, patientHistory.Prescription, patientHistory.Description, patientHistory.Recomandations);
                    _repository.EditPatientHistory(entity);
                }
            }
            catch (InvalidCastException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit Patient History.");
            }

            return View(entity);
        }

        // GET: PatientHistories/Delete/5
        public IActionResult Delete(Guid id)
        {
            var patientHistory = _repository.GetPatientHistoryById(id);

            if (patientHistory == null)
            {
                return NotFound();
            }

            return View(patientHistory);
        }

        // POST: PatientHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _repository.DeletePatientHistory(id);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete patient.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
