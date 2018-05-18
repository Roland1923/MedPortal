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
    [Route("api/BloodDonors")]
    public class BloodDonorsController : Controller
    {
        private readonly IEditableRepository<BloodDonor> _repository;


        public BloodDonorsController(IEditableRepository<BloodDonor> repository)
        {
            _repository = repository;
        }

        // GET api/BloodDonors
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<BloodDonor>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodDonors()
        {
            try
            {
                var bloodDonors = await _repository.GetAllAsync(null);
                return Ok(bloodDonors);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/BloodDonors/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<BloodDonor>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodDonorsPage(int skip, int take)
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

        // GET api/BloodDonors/5
        [HttpGet("{id}", Name = "GetBloodDonorRoute")]
        [NoCache]
        [ProducesResponseType(typeof(BloodDonor), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodDonors(Guid id)
        {
            try
            {
                var bloodDonor = await _repository.GetByIdAsync(id);
                return Ok(bloodDonor);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/BloodDonors
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateBloodDonor([FromBody]BloodDonorModel bloodDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = BloodDonor.Create(bloodDonor.Type, bloodDonor.PatientId);

            try
            {
                var newBloodDonor = await _repository.AddAsync(instance);
                if (newBloodDonor == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetBloodDonorRoute", new { id = newBloodDonor.BloodDonorId },
                    new ApiResponse { Status = true, BloodDonor = newBloodDonor });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/BloodDonors/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateBloodDonor(Guid id, [FromBody]BloodDonorModel bloodDonor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(bloodDonor.Type, bloodDonor.PatientId);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, BloodDonor = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/BloodDonors/5
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteBloodDonor(Guid id)
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