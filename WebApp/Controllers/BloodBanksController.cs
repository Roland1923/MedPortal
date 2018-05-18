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
    [Route("api/BloodBanks")]
    public class BloodBanksController : Controller
    {
        private readonly IEditableRepository<BloodBank> _repository;


        public BloodBanksController(IEditableRepository<BloodBank> repository)
        {
            _repository = repository;
        }

        // GET api/BloodBanks
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<BloodBank>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodBanks()
        {
            try
            {
                var BloodBanks = await _repository.GetAllAsync(null);
                return Ok(BloodBanks);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/BloodBanks/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<BloodBank>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodBanksPage(int skip, int take)
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

        // GET api/BloodBanks/5
        [HttpGet("{id}", Name = "GetBloodBankRoute")]
        [NoCache]
        [ProducesResponseType(typeof(BloodBank), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> BloodBanks(Guid id)
        {
            try
            {
                var bloodBank = await _repository.GetByIdAsync(id);
                return Ok(bloodBank);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/BloodBanks
        [HttpPost]
        // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateBloodBank([FromBody]BloodBankModel bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = BloodBank.Create(bloodBank.Doctor);
            try
            {
                var newBloodBank = await _repository.AddAsync(instance);
                if (newBloodBank == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetBloodBankRoute", new { id = newBloodBank.BloodBankId },
                    new ApiResponse { Status = true, BloodBank = newBloodBank });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/BloodBanks/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateBloodBank(Guid id, [FromBody]BloodBankModel bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(bloodBank.Type, bloodBank.Doctor);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, BloodBank = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/BloodBanks/5
        [HttpDelete("{id}")]
        // [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteBloodBank(Guid id)
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