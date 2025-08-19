using ShiftsLogger.Data.DTOs.Contracts;

namespace ShiftsLogger.Data.DTOs
{
    public class WorkerDto : IWorkerDto
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
