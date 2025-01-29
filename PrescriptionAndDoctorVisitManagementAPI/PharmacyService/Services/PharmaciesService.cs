using PharmacyService.DTOs;
using PharmacyService.Entities;
using PharmacyService.Repositories;
using PharmacyService.Token;
using PrescriptionService.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyService.Services
{
    public class PharmaciesService : IPharmacyService
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IConfiguration _configuration;

        public PharmaciesService(IPharmacyRepository pharmacyRepository, IConfiguration configuration)
        {
            _pharmacyRepository = pharmacyRepository;
            _configuration = configuration;
        }

        public async Task CreatePharmacyAsync(CreatePharmacyDTO dto)
        {
            var pharmacy = new Pharmacy
            {
                PharmacyId = Guid.NewGuid(),
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _pharmacyRepository.CreateAsync(pharmacy);
        }

        public async Task<string> AuthenticateAsync(LoginPharmacyDTO dto)
        {
            var pharmacy = await _pharmacyRepository.GetByUsernameAsync(dto.Username);
            if (pharmacy == null || !VerifyPassword(dto.Password, pharmacy.PasswordHash))
            {
                return null;
            }

            var token = TokenHandler.CreateToken(_configuration, pharmacy.PharmacyId);
            return token.AccessToken;
        }

        public async Task HandlePrescriptionAsync(HandlePrescriptionDTO dto)
        {
            await _pharmacyRepository.UpdatePrescriptionAsync(dto);
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

        public Task UpdatePrescriptionAsync(HandlePrescriptionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Prescription>> GetPrescriptionsAsync(Guid pharmacyId)
        {
            throw new NotImplementedException();
        }
    }
}
