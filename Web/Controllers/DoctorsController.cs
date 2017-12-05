using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.IRepositories;
using Web.Models;

namespace Web.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _repository;

        public DoctorsController(IDoctorRepository repository)
        {
            _repository = repository;
        }

        // GET: Doctors
        public IActionResult Index()
        {
            return View(_repository.GetAllDoctors());
        }

        // GET: Doctors/Details/5
        public IActionResult Details(Guid id)
        {
            var doctor = _repository.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,Password,PhoneNumber,Speciality,Hospital,City,Address")] DoctorModel doctor)
        {
            var instance = Doctor.Create(doctor.FirstName, doctor.LastName, doctor.Email, doctor.Password, doctor.PhoneNumber, doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address);

            if (ModelState.IsValid)
            {
                _repository.AddDoctor(instance);

                return RedirectToAction(nameof(Index));
            }
            return View(instance);
        }

        // GET: Doctors/Edit/5
        public IActionResult Edit(Guid id)
        {
            var doctor = _repository.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("FirstName,LastName,Email,Password,PhoneNumber,Speciality,Hospital,City,Address")] DoctorModel doctor)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var entity = _repository.GetDoctorById(id);

            try
            {
                if (ModelState.IsValid)
                {
                    entity.Update(doctor.FirstName, doctor.LastName, doctor.Email, doctor.Password, doctor.PhoneNumber, doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address);
                    _repository.EditDoctor(entity);
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit doctor.");
            }

            return View(entity);
        }

        // GET: Doctors/Delete/5
        public IActionResult Delete(Guid id)
        {
            var doctor = _repository.GetDoctorById(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _repository.DeleteDoctor(id);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete doctor.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
