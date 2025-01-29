using Quartz;

namespace PrescriptionService.Schedulers
{
    public class MedicineUpdateJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Updating Medicine List");
        }
    }
}