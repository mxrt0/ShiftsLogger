namespace ShiftsLogger.Data.DTOs
{
    public class CreateWorkerDto
    {
        public CreateWorkerDto(string name)
        {
            Name = name;
        }
        public CreateWorkerDto() { }
        public string Name { get; set; }
    }
}
