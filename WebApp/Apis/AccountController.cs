using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApp.Common;
using WebApp.Models;

namespace WebApp.Apis
{
  [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
      private readonly IEditableRepository<Doctor> _repositoryDoctor;
      private readonly IEditableRepository<Patient> _repositoryPatient;



      public AccountController(IEditableRepository<Doctor> repositoryDoctor,IEditableRepository<Patient> repositoryPatient )
      {
        _repositoryDoctor = repositoryDoctor;
        _repositoryPatient = repositoryPatient;
      }

     // api/Accout/doctorAccount
      [HttpPut("doctorAccount")]
      [NoCache]
      [ProducesResponseType(typeof(Doctor), 200)]
      [ProducesResponseType(typeof(ApiResponse), 404)]
      public async Task<ActionResult> DoctorAccount([FromBody]CredetialsModel doctorCredetialsModel)
      {
        
      
        try
        {
          var doctorsList = await _repositoryDoctor.GetAllAsync();
          if (!doctorsList.Any(doctor => doctor.Email == doctorCredetialsModel.Email && doctor.Password == doctorCredetialsModel.Password))
            return Json(new RequestResult
            {
              State = RequestState.Failed
            });
        //return BadRequest(new ApiResponse {Status = false});
        var requestAt = DateTime.Now;
          var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
          var token = GenerateToken(expiresIn);

          return Json(new RequestResult
          {
            State = RequestState.Success,
            Data = new
            {
              requertAt = requestAt,
              expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
              tokeyType = TokenAuthOption.TokenType,
              accessToken = token
            }
          });
        }
        catch
        {
          return Json(new RequestResult
          {
            State = RequestState.Failed
          });
        //return BadRequest(new ApiResponse { Status = false });
        }
      }

    [HttpGet("patientAccount")]
    [NoCache]
    [ProducesResponseType(typeof(Patient), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult> PacientAccount(string email, string password)
      {
        try
        {
          var pacientsList = await _repositoryPatient.GetAllAsync();
          if (!pacientsList.Any(pacient => pacient.Email == email && pacient.Password == password))
            // return BadRequest(new ApiResponse {Status = false});
          {
            return Json(new RequestResult
            {
              State = RequestState.Failed
            });
          }
          var requestAt = DateTime.Now;
          var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
          var token = GenerateToken(expiresIn);

          return Json(new RequestResult
          {
            State = RequestState.Success,
            Data = new
            {
              requertAt = requestAt,
              expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
              tokeyType = TokenAuthOption.TokenType,
              accessToken = token
            }
          });
        }
        catch
        {
          return BadRequest(new ApiResponse { Status = false });
        }
      }



    private string GenerateToken(DateTime expires)
      {
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
          Issuer = TokenAuthOption.Issuer,
          Audience = TokenAuthOption.Audience,
          SigningCredentials = TokenAuthOption.SigningCredentials,

          Expires = expires
        });
        return handler.WriteToken(securityToken);
      }
      [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
      [ProducesResponseType(typeof(ApiResponse), 200)]
      [HttpGet("GetStaff")]
      public IActionResult GetStaff()
      {
      return BadRequest(new ApiResponse { Status = false });
    }
  }
}