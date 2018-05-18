using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
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
      public async Task<ActionResult> DoctorAccount([FromBody]CredentialsModel doctorCredetialsModel)
      {
        try
        {
          MD5 md5Hash = MD5.Create();
          string passwordHash = PasswordHashMd5.GetMd5Hash(md5Hash, doctorCredetialsModel.Password);
          string[] includes = {"Appointments","Feedbacks"};
          var doctorsList = await _repositoryDoctor.GetAllAsync(null);
          foreach (var doctor in doctorsList)
          {
            if (doctor.Email == doctorCredetialsModel.Email && doctor.Password == passwordHash)
            {
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
                  accessToken = token,
                  user_id = doctor.DoctorId       
                }
              });
            }
          }
        }
        catch
        {
          return Json(new RequestResult
          {
            State = RequestState.Failed
          });
          
        }
        return Json(new RequestResult
        {
          State = RequestState.Failed
        });

    }

      [HttpPut("patientAccount")]
      [NoCache]
      [ProducesResponseType(typeof(Patient), 200)]
      [ProducesResponseType(typeof(ApiResponse), 400)]
      public async Task<ActionResult> PacientAccount([FromBody] CredentialsModel patientCredetialsModel)
      {
        try
        {
          MD5 md5Hash = MD5.Create();
          string passwordHash = PasswordHashMd5.GetMd5Hash(md5Hash, patientCredetialsModel.Password);
          string[] includes = { "Appointments", "Feedbacks", "PatientHistories" };
          var patientList = await _repositoryPatient.GetAllAsync(null);
          foreach (var patient in patientList)
          {
            if (patient.Email == patientCredetialsModel.Email && patient.Password == passwordHash)
            {
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
                  accessToken = token,
                  user_id = patient.PatientId
                }
              });
            }
          }
        }
        catch
        {
          return Json(new RequestResult
          {
            State = RequestState.Failed
          });

        }
        return Json(new RequestResult
        {
          State = RequestState.Failed
        });

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