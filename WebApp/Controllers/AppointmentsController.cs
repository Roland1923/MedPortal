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
    [Route("api/Appointments")]
    public class AppointmentsController : Controller
    {
        private readonly IEditableRepository<Appointment> _repository;
        
        public AppointmentsController(IEditableRepository<Appointment> repository)
        {
            _repository = repository;
        }

        // GET api/Appointments
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Appointment>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Appointments()
        {
            try
            {
                string[] includes = { "Patient", "Doctor" };
                var appointments = await _repository.GetAllAsync(null);
                return Ok(appointments);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/Appointments/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Appointment>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> AppointmentsPage(int skip, int take)
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

        // GET api/Appointments/5
        [HttpGet("{id}")]
        [NoCache]
        [ProducesResponseType(typeof(Appointment), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Appointments(Guid id)
        {
            try
            {
                var appointment = await _repository.GetByIdAsync(id);
                return Ok(appointment);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/Appointments
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateAppointment([FromBody]AppointmentModel appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = Appointment.Create(appointment.AppointmentDate, appointment.DoctorId, appointment.PatientId);
            
            try
            {
                var newAppointment = await _repository.AddAsync(instance);
                if (newAppointment == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetAppointmentRoute", new { id = newAppointment.AppointmentId },
                    new ApiResponse { Status = true, Appointment = newAppointment });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/Appointments/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateAppointment(Guid id, [FromBody]AppointmentModel appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(appointment.AppointmentDate, appointment.DoctorId, appointment.PatientId);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, Appointment = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/Appointments/5
        [HttpDelete("{id}")]
       // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteAppointment(Guid id)
        {
            try
            {
                var status = await _repository.DeleteAsync(id);
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