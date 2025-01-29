namespace PrescriptionService.DTOs
{
    public class GetPrescriptionsDTO
    {
        public Guid PrescriptionId { get; set; }
        public Guid? DoctorId { get; set; }
        public string? TCNumber { get; set; }
        public string? Status { get; set; }
    }
}
