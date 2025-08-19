using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.Data.Entities
{
    public class Worker
    {
        public Worker(string name)
        {
            Name = name;
            Shifts = new();
        }

        public Worker()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<Shift> Shifts { get; set; }
    }
}
