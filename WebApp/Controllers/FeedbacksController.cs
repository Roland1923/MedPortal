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
    [Route("api/Feedbacks")]
    public class FeedbacksController : Controller
    {
        private readonly IEditableRepository<Feedback> _repository;


        public FeedbacksController(IEditableRepository<Feedback> repository)
        {
            _repository = repository;
        }

        // GET api/Feedbacks
        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Feedback>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Feedbacks()
        {
            try
            {
                var feedbacks = await _repository.GetAllAsync(null);
                return Ok(feedbacks);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }


        // GET api/Feedbacks/page/10/10
        [HttpGet("page/{skip}/{take}")]
        [NoCache]
        [ProducesResponseType(typeof(List<Feedback>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> FeedbacksPage(int skip, int take)
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

        // GET api/Feedbacks/5
        [HttpGet("{id}", Name = "GetFeedbackRoute")]
        [NoCache]
        [ProducesResponseType(typeof(Feedback), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Feedbacks(Guid id)
        {
            try
            {
                var feedback = await _repository.GetByIdAsync(id);
                return Ok(feedback);
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // POST api/Feedbacks
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateFeedback([FromBody]FeedbackModel feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = Feedback.Create(feedback.Description, feedback.PatientId, feedback.DoctorId, feedback.Rating);

            try
            {
                var newFeedback = await _repository.AddAsync(instance);
                if (newFeedback == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return CreatedAtRoute("GetFeedbackRoute", new { id = newFeedback.FeedbackId },
                    new ApiResponse { Status = true, Feedback = newFeedback });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // PUT api/Feedbacks/5
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateFeedback(Guid id, [FromBody]FeedbackModel feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instance = await _repository.GetByIdAsync(id);

            try
            {

                instance.Update(feedback.Description, feedback.PatientId, feedback.DoctorId, feedback.Rating);

                var status = await _repository.UpdateAsync(instance);
                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }
                return Ok(new ApiResponse { Status = true, Feedback = instance });
            }
            catch
            {
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        // DELETE api/Feedbacks/5
        [HttpDelete("{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteFeedback(Guid id)
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