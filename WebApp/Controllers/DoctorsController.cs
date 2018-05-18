using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Attributes;
using WebApp.Common;
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
              string[] includes = { "Appointments", "Feedbacks" };
              var doctors = await _repository.GetAllAsync(null);
              return Ok(doctors);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }
        
        
        // GET api/Doctors/page/name/10/10
        [HttpPut("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Doctor>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DoctorsNamePage([FromBody]DoctorFilterModel filter, int skip, int take)
        {
            try
            {
                var pagingResult = await _repository.GetByFilter(FilterDelegate(filter.Name, filter.Hospital, filter.Speciality, filter.City), skip, take);
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
        public async Task<ActionResult> CreateDoctor([FromBody]DoctorModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MD5 md5Hash = MD5.Create();
            string passwordHash = PasswordHashMd5.GetMd5Hash(md5Hash, doctor.Password);

            var instance = Doctor.Create(doctor.FirstName, doctor.LastName, doctor.Email, passwordHash, doctor.PhoneNumber, doctor.Description, doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address);

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
        public async Task<ActionResult> UpdateDoctor(Guid id, [FromBody]DoctorModel doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MD5 md5Hash = MD5.Create();
            string passwordHash = PasswordHashMd5.GetMd5Hash(md5Hash, doctor.Password);

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(doctor.FirstName, doctor.LastName, doctor.Email, passwordHash, doctor.PhoneNumber, doctor.Description,doctor.Speciality, doctor.Hospital, doctor.City, doctor.Address);

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
        public async Task<ActionResult> DeleteDoctor(Guid id)
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

        private static Expression<Func<Doctor, bool>> FilterDelegate(string name, string hospital, string speciality, string city)
        {
            if (name != "" && hospital != "" && speciality != "" && city != "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                            x.Speciality.Contains(speciality) && x.Hospital.Contains(hospital) && x.City.Contains(city);

            if (name != "" && hospital != "" && speciality != "" && city == "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                            x.Speciality.Contains(speciality) && x.Hospital.Contains(hospital);

            if (name != "" && hospital != "" && speciality == "" && city != "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                            x.Speciality.Contains(speciality) && x.City.Contains(city);

            if (name != "" && hospital == "" && speciality != "" && city != "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                             x.Hospital.Contains(hospital) && x.City.Contains(city);

            if (name == "" && hospital != "" && speciality != "" && city != "")
                return x => x.Speciality.Contains(speciality) && x.Hospital.Contains(hospital) && x.City.Contains(city);

            if (name == "" && hospital == "" && speciality != "" && city != "")
                return x => x.Speciality.Contains(speciality) && x.City.Contains(city);

            if (name == "" && hospital != "" && speciality == "" && city != "")
                return x => x.Hospital.Contains(hospital) && x.City.Contains(city);

            if (name == "" && hospital != "" && speciality != "" && city == "")
                return x => x.Speciality.Contains(speciality) && x.Hospital.Contains(hospital);

            if (name != "" && hospital == "" && speciality == "" && city != "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) && x.City.Contains(city);

            if (name != "" && hospital == "" && speciality != "" && city == "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                            x.Speciality.Contains(speciality);

            if (name != "" && hospital != "" && speciality == "" && city == "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name)) &&
                            x.Hospital.Contains(hospital);

            if (name != "" && hospital == "" && speciality == "" && city == "")
                return x => (x.LastName.Contains(name) || x.FirstName.Contains(name));

            if (name == "" && hospital != "" && speciality == "" && city == "")
                return x => x.Hospital.Contains(hospital);

            if (name == "" && hospital == "" && speciality != "" && city == "")
                return x => x.Speciality.Contains(speciality);

            if (name == "" && hospital == "" && speciality == "" && city != "")
                return x => x.City.Contains(city);

            return x => x.LastName.Contains(name) || x.FirstName.Contains(name);
        }

    }
}