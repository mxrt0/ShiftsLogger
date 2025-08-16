namespace ShiftsLogger.API.DTOs
{
    public class WorkerDto
    {
        public WorkerDto(string name)
        {
            Name = name;
        }
        public WorkerDto() { }
        public string Name { get; set; }
    }
}
