using System;
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
    [Route("api/Doctors")]
    public class DoctorsController : Controller
    {
        private readonly IEditableRepository<Doctor> _repository;


        public DoctorsController(IEditableRepository<Doctor> repository)
        {
            _repository = repository;
        }

        // GET api/Doctors
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Doctor>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Doctors()
        {
            try
            {
             
              var doctors = await _repository.GetAllAsync();
              return Ok(doctors);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

   


    // GET api/Doctors/page/10/10
    [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Doctor>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DoctorsPage(int skip, int take)
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

        // GET api/Doctors/5
        [HttpGet("{id}", Name = "GetDoctorRoute")]
        [NoCache]
        [ProducesResponseType(typeof(Doctor), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Doctors(Guid id)
        {
            try
            {
                var doctor = await _repository.GetByIdAsync(id);
                return Ok(doctor);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/Doctors
        [HttpPost]
       // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateDoctor([FromBody]CreateDoctorModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = Doctor.Create(doctor.FirstName, doctor.LastName, doctor.Email, doctor.Password, doctor.PhoneNumber, doctor.Description, doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address);

            try
            {
                var newDoctor = await _repository.AddAsync(instance);
                if (newDoctor == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetDoctorRoute", new { id = newDoctor.DoctorId },
                    new ApiResponse { Status = true, Doctor = newDoctor });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/Doctors/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateDoctor(Guid id, [FromBody]UpdateDoctorModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(doctor.FirstName, doctor.LastName, doctor.Email, doctor.Password, doctor.PhoneNumber, doctor.Description,doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address, doctor.Appointments, doctor.Feedbacks);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, Doctor = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/Doctors/5
        [HttpDelete("{id}")]
       // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteDoctor([FromBody]Doctor doctor)
        {
            try
            {
                var status = await _repository.DeleteAsync(doctor);
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