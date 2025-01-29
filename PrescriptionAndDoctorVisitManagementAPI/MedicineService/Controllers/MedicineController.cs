using MedicineService.DTOs;
using MedicineService.Entities;
using MedicineService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicineService.Controllers
{
    [Route("api/v1/medicines")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMedicines([FromQuery] string query)
        {
            var medicines = await _medicineService.SearchMedicinesAsync(query);
            return Ok(medicines);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateMedicines([FromBody] List<UpdateMedicineDTO> medicines)
        {
            await _medicineService.UpdateMedicinesAsync(medicines);
            return Ok();
        }
    }
}
