namespace ShiftsLogger.Data.DTOs
{
    public class WorkerDto
    {
        public WorkerDto(string name)
        {
            Name = name;
        }
        public WorkerDto() { }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
