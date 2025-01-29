using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrescriptionService.DTOs;
using PrescriptionService.Entities;
using PrescriptionService.Services;

namespace PrescriptionService.Controllers
{
    [ApiController]
    [Route("api/v1/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("GetPrescription")]
        [Authorize]
        public async Task<IActionResult> GetPrescriptions([FromQuery] GetPrescriptionsDTO filter)
        {
            var prescriptions = await _prescriptionService.GetPrescriptionsAsync(filter);
            return Ok(prescriptions);
        }

        [HttpPut("UpdatePrescription")]
        [Authorize]
        public async Task<IActionResult> UpdatePrescription(UpdatePrescriptionDTO dto)
        {
            await _prescriptionService.UpdatePrescriptionAsync(dto);
            return Ok(new {message = "Prescription updated successfully."});
        }

        [HttpPost("Queue")]
        public async Task<IActionResult> QueuePrescription(Prescription prescription)
        {
            await _prescriptionService.QueuePrescriptionAsync(prescription);
            return Ok();
        }

        [HttpGet("{id}/missing-medicines")]
        public async Task<IActionResult> CheckMissingMedicines(Guid id)
        {
            await _prescriptionService.CheckMissingMedicines(id);
            return Ok();
        }
    }
}
