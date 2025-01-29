namespace PrescriptionService.DTOs
{
    public class PrescriptionDto
    {
        public string PrescriptionId { get; set; } 
        public string DoctorId { get; set; }      
        public string TcNumber { get; set; }       
        public List<PrescriptionMedicineDto> Medicines { get; set; } 
        public string Status { get; set; }         
        public DateTime CreatedAt { get; set; }    
    }

    public class PrescriptionMedicineDto
    {
        public string Name { get; set; }    
        public int Quantity { get; set; }    
        public decimal Price { get; set; }  
    }
}
