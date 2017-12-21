﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Attributes;
using WebApp.Models;

namespace WebApp.Apis
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Controller
    {
        private readonly IEditableRepository<Patient> _repository;


        public PatientsController(IEditableRepository<Patient> repository)
        {
            _repository = repository;
        }

        // GET api/patients
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Patient>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Patients()
        {
            try
            {
                var patients = await _repository.GetAllAsync();
                return Ok(patients);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/patients/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Patient>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> PatientsPage(int skip, int take)
        {
            try
            {
                var pagingResult = await _repository.GetAllPageAsync(skip, take);
                Response.Headers.Add("X-InlineCount", pagingResult.TotalRecords.ToString());
                return Ok(pagingResult.Records);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // GET api/patients/5
        [HttpGet("{id}", Name = "GetPatientRoute")]
        [NoCache]
        [ProducesResponseType(typeof(Patient), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Patients(Guid id)
        {
            try
            {
                var patient = await _repository.GetByIdAsync(id);
                return Ok(patient);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/patients
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreatePatient([FromBody]CreatePatientModel patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = Patient.Create(patient.FirstName, patient.LastName, patient.Email, patient.Password, patient.City, patient.Birthdate, patient.PhoneNumber, null);

            try
            {
                var newPatient = await _repository.AddAsync(instance);
                if (newPatient == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetPatientRoute", new { id = newPatient.PatientId },
                    new ApiResponse { Status = true, Patient = newPatient });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/patients/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdatePatient(Guid id, [FromBody]UpdatePatientModel patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(patient.FirstName, patient.LastName, patient.Email, patient.Password, patient.City, patient.Birthdate, patient.PhoneNumber, patient.BloodDonor, patient.Appointments, patient.PatientHistories, patient.Feedbacks);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, Patient = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/patients/5
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeletePatient([FromBody]Patient patient)
        {
            try
            {
                var status = await _repository.DeleteAsync(patient);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


    }
}