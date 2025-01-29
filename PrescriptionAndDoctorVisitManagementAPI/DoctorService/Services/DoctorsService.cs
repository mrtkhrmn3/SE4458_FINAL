using AutoMapper;
using DoctorService.DTOs;
using DoctorService.Entities;
using DoctorService.Repositories;
using DoctorService.Token;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DoctorService.Services
{
    public class DoctorsService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IConfiguration _configuration;

        public DoctorsService(IDoctorRepository doctorRepository, IPatientRepository patientRepository,  IVisitRepository visitRepository, IConfiguration configuration)
        {

            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _visitRepository = visitRepository;
            _configuration = configuration;
        }

        public async Task CreateDoctorAsync(CreateDoctorDTO dto)
        {
            var doctor = new Doctor
            {
                DoctorId = Guid.NewGuid(),
                Name = dto.Name,
                Specialization = dto.Specialization,
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _doctorRepository.CreateAsync(doctor);
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var doctor = await _doctorRepository.GetByUsernameAsync(dto.Username);
            if (doctor == null || !VerifyPassword(dto.Password, doctor.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            var token = TokenHandler.CreateToken(_configuration, doctor.DoctorId);
            return token.AccessToken;
        }

        public async Task CreateVisitAsync(CreateVisitDTO dto, Guid doctorId)
        {
            var patient = await _patientRepository.GetByTCNumberAsync(dto.TCNumber);
            if (patient == null)
            {
                patient = new Patient
                {
                    TCNumber = dto.TCNumber,
                    Name = dto.FullName,
                    CreatedAt = DateTime.UtcNow
                };
                await _patientRepository.CreateAsync(patient);
            }

            var visit = new Visit
            {
                VisitId = Guid.NewGuid(),
                DoctorId = doctorId,
                TCNumber = dto.FullName,
                VisitDate = dto.VisitDate,
                Notes = dto.Notes
            };

            await _visitRepository.CreateAsync(visit);
        }

        public async Task CreatePrescriptionAsync(CreatePrescriptionDTO dto)
        {
            // Reçete oluşturulması ve PrescriptionService'e gönderilmesi
            var prescription = new Prescription
            {
                PrescriptionId = Guid.NewGuid(),
                DoctorId = dto.DoctorId,
                TCNumber = dto.TCNumber,
                Medicines = dto.Medicines.Select(m => new Medicine
                {
                    Name = m.Name,
                    Quantity = m.Quantity,
                    Price = m.Price
                }).ToList(),
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await _doctorRepository.CreatePrescriptionAsync(prescription);
        }

        public async Task<bool> DoctorExistsAsync(Guid doctorId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            return doctor != null;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hash = HashPassword(password);
            return hash == hashedPassword;
        }
    }
}