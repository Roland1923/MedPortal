using System;
using System.Data;
using Core.Entities;
using Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentsController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View(_repository.GetAllAppointments());

        }

        // GET: Patients/Details/e53cec5f-5632-4d98-bc66-23951b373468
        public IActionResult Details(Guid id)
        {
            var appointment = _repository.GetAppointmentById(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
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
        public IActionResult Create([Bind("AppointmentDate,PatientForAppointment")] AppointmentModel appointment)
        {
            if (ModelState.IsValid)
            {
                var instance = Appointment.Create(appointment.AppointmentDate,appointment.PatientForAppointment);
                _repository.AddAppointment(instance);

                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Patients/Edit/e53cec5f-5632-4d98-bc66-23951b373468
        public IActionResult Edit(Guid id)
        {
            var appointment = _repository.GetAppointmentById(id);

            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("AppointmentDate,PatientForAppointment")] AppointmentModel appointment)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var entity = _repository.GetAppointmentById(id);

            try
            {
                if (ModelState.IsValid)
                {
                    entity.Update(appointment.AppointmentDate,appointment.PatientForAppointment);
                    _repository.EditAppointment(entity);
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit appointment.");
            }

            return View(entity);
        }

        // GET: Patients/Delete/5
        public IActionResult Delete(Guid id)
        {
            var patient = _repository.GetAppointmentById(id);

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
                _repository.DeleteAppointment(id);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete appointment.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
