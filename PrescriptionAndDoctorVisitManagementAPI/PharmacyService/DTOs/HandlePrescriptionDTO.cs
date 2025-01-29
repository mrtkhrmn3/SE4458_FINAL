namespace PharmacyService.DTOs
{
    public class HandlePrescriptionDTO
    {
        public Guid PrescriptionId { get; set; }
        public List<string> MissingMedicines { get; set; }
    }
}
