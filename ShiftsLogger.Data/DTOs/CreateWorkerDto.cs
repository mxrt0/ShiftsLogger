namespace ShiftsLogger.Data.DTOs
{
    public class CreateWorkerDto : WorkerDto
    {
        public CreateWorkerDto(string name)
        {
            Name = name;
        }
        public CreateWorkerDto() { }
    }
}
