namespace DoctorService.DTOs
{
    public class CreatePrescriptionDTO
    {
        public Guid DoctorId { get; set; }
        public string TCNumber { get; set; }
        public List<MedicineDTO> Medicines { get; set; }
    }

    public class MedicineDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
