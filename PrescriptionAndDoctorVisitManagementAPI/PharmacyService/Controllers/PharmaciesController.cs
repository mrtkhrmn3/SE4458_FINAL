using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyService.DTOs;
using PharmacyService.Services;

namespace PharmacyService.Controllers
{
    [ApiController]
    [Route("api/v1/pharmacies")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreatePharmacy([FromBody] CreatePharmacyDTO dto)
        {
            await _pharmacyService.CreatePharmacyAsync(dto);
            return Ok(new { message = "Pharmacy created successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginPharmacyDTO dto)
        {
            var token = await _pharmacyService.AuthenticateAsync(dto);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }

        [HttpPost("prescriptions")]
        [Authorize]
        public async Task<IActionResult> HandlePrescriptions(HandlePrescriptionDTO dto)
        {
            await _pharmacyService.HandlePrescriptionAsync(dto);
            return Ok("Prescription handled successfully.");
        }
    }
}
