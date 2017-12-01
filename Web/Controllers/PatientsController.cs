using System;
using System.Data;
using Core.Entities;
using Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _repository;

        public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View(_repository.GetAllPatients());

        }

        // GET: Patients/Details/e53cec5f-5632-4d98-bc66-23951b373468
        public IActionResult Details(Guid id)
        {
            var patient = _repository.GetPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName, LastName, Email, City, Birthdate, PhoneNumber")] PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                var instance = Patient.Create(patient.FirstName, patient.LastName, patient.Email, patient.City, patient.Birthdate, patient.PhoneNumber);
                _repository.AddPatient(instance);

                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/e53cec5f-5632-4d98-bc66-23951b373468
        public IActionResult Edit(Guid id)
        {
            var patient = _repository.GetPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FirstName, LastName, Email, City, Birthdate, PhoneNumber")] PatientModel patient)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var entity = _repository.GetPatientById(id);

            try
            {
                if (ModelState.IsValid)
                {
                    entity.Update(patient.FirstName, patient.LastName, patient.Email, patient.City, patient.Birthdate, patient.PhoneNumber);
                    _repository.EditPatient(entity);
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit patient.");
            }

            return View(entity);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid id)
        {
            var patient = _repository.GetPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _repository.DeletePatient(id);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete patient.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
