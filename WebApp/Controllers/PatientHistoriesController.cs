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
    [Route("api/PatientHistorys")]
    public class PatientHistoriesController : Controller
    {
        private readonly IEditableRepository<PatientHistory> _repository;


        public PatientHistoriesController(IEditableRepository<PatientHistory> repository)
        {
            _repository = repository;
        }

        // GET api/PatientHistorys
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<PatientHistory>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> PatientHistorys()
        {
            try
            {
                var patientHistories = await _repository.GetAllAsync(null);
                return Ok(patientHistories);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/PatientHistorys/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<PatientHistory>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> PatientHistorysPage(int skip, int take)
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

        // GET api/PatientHistorys/5
        [HttpGet("{id}", Name = "GetPatientHistoryRoute")]
        [NoCache]
        [ProducesResponseType(typeof(PatientHistory), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> PatientHistorys(Guid id)
        {
            try
            {
                var patientHistory = await _repository.GetByIdAsync(id);
                return Ok(patientHistory);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/PatientHistorys
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreatePatientHistory([FromBody]PatientHistoryModel patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = PatientHistory.Create(patientHistory.PatientId, patientHistory.DoctorId, patientHistory.Prescription, patientHistory.Description, patientHistory.Recommendations, patientHistory.Date);

            try
            {
                var newPatientHistory = await _repository.AddAsync(instance);
                if (newPatientHistory == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetPatientHistoryRoute", new { id = newPatientHistory.HistoryId },
                    new ApiResponse { Status = true, PatientHistory = newPatientHistory });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/PatientHistorys/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdatePatientHistory(Guid id, [FromBody]PatientHistoryModel patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(patientHistory.PatientId, patientHistory.DoctorId, patientHistory.Prescription, patientHistory.Description, patientHistory.Recommendations, patientHistory.Date);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, PatientHistory = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/PatientHistorys/5
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeletePatientHistory(Guid id)
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