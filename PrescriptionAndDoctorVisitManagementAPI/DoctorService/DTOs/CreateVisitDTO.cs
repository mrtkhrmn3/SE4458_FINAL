namespace DoctorService.DTOs
{
    public class CreateVisitDTO
    {
        public Guid DoctorId { get; set; }
        public string TCNumber { get; set; }
        public string FullName { get; set; }
        public DateTime VisitDate { get; set; }
        public string Notes { get; set; }
    }
}
