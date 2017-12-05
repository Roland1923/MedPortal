using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.IRepositories;
using Web.Models;

namespace Web.Controllers
{
    public class BloodTypesController : Controller
    {
        private readonly IBloodTypeRepository _repository;

        public BloodTypesController(IBloodTypeRepository repository)
        {
            _repository = repository;
        }

        // GET: BloodTypes
        public IActionResult Index()
        {
            return View(_repository.GetAllBloodTypes());
        }

        // GET: BloodTypes/Details/5
        public IActionResult Details(Guid id)
        {
            var bloodType = _repository.GetBloodTypeById(id);

            if (bloodType == null)
            {
                return NotFound();
            }

            return View(bloodType);
        }

        // GET: BloodTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BloodTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Type")] BloodTypeModel bloodType)
        {
            var instance = BloodType.Create(bloodType.Type);

            if (ModelState.IsValid)
            {
                _repository.AddBloodType(instance);

                return RedirectToAction(nameof(Index));
            }
            return View(instance);
        }

        // GET: BloodTypes/Edit/5
        public IActionResult Edit(Guid id)
        {
            var bloodType = _repository.GetBloodTypeById(id);

            if (bloodType == null)
            {
                return NotFound();
            }
            return View(bloodType);
        }

        // POST: BloodTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Type")] BloodTypeModel bloodType)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var entity = _repository.GetBloodTypeById(id);

            try
            {
                if (ModelState.IsValid)
                {
                    entity.Update(bloodType.Type);
                    _repository.EditBloodType(entity);
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "Unable to edit bloodType.");
            }

            return View(entity);
        }

        // GET: BloodTypes/Delete/5
        public IActionResult Delete(Guid id)
        {
            var bloodType = _repository.GetBloodTypeById(id);

            if (bloodType == null)
            {
                return NotFound();
            }

            return View(bloodType);
        }

        // POST: BloodTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _repository.DeleteBloodType(id);
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete bloodType.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
