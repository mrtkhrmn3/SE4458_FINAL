using Microsoft.AspNetCore.Connections;
using PrescriptionService.DTOs;
using PrescriptionService.Entities;
using PrescriptionService.Repositories;
using RabbitMQ.Client;
using System.Text.Json;

namespace PrescriptionService.Services
{
    public class PrescriptionsService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionsService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<List<Prescription>> GetPrescriptionsAsync(GetPrescriptionsDTO filter)
        {
            return await _prescriptionRepository.GetPrescriptionsAsync(filter);
        }

        public async Task UpdatePrescriptionAsync(UpdatePrescriptionDTO dto)
        {
            await _prescriptionRepository.UpdatePrescriptionAsync(dto);
        }
        
        public async Task QueuePrescriptionAsync(Prescription prescription)
        {
            var factory = new ConnectionFactory { HostName = "127.0.0.1", Port=5672, UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "prescriptions", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(prescription);
            var body = System.Text.Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: "prescriptions", basicProperties: null, body: body);
        }

        public async Task CheckMissingMedicines(Guid prescriptionId)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionsAsync(new GetPrescriptionsDTO { PrescriptionId = prescriptionId });
            if (prescription == null || !prescription.Any())
                throw new Exception("Prescription not found.");

        }
    }
}
