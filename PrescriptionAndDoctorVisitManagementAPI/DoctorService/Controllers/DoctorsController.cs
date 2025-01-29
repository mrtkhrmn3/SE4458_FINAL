using DoctorService.DTOs;
using DoctorService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace DoctorService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDTO dto)
        {
            await _doctorService.CreateDoctorAsync(dto);
            return Ok(new { message = "Doctor created successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _doctorService.LoginAsync(dto);
            return Ok(new { AccessToken = token });
        }

        [HttpPost("visits")]
        [Authorize]
        public async Task<IActionResult> CreateVisit([FromBody] CreateVisitDTO dto)
        {
            var doctorId = dto.DoctorId;

            var doctorExists = await _doctorService.DoctorExistsAsync(doctorId);
            if (!doctorExists)
            {
                return NotFound($"Doctor with ID {doctorId} does not exist.");
            }

            await _doctorService.CreateVisitAsync(dto, doctorId);
            return Ok(new { message = "Visit created successfully." });
        }

        [HttpPost("prescriptions")]
        [Authorize]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionDTO dto)
    {
        var doctorId = dto.DoctorId;
        var doctorExists = await _doctorService.DoctorExistsAsync(doctorId);
        if (!doctorExists)
        {
            return NotFound($"Doctor with ID {doctorId} does not exist.");
        }

        await _doctorService.CreatePrescriptionAsync(dto);
        return Ok(new { message = "Prescription created successfully." });
    }
}
}
