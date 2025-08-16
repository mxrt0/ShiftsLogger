using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.API.DTOs
{
    public class ShiftDto
    {
        public ShiftDto(DateTime date, DateTime start, DateTime end, int workerId)
        {
            Date = date;
            Start = start;
            End = end;
            WorkerId = workerId;
            Duration = End - Start;
        }
        public ShiftDto() { }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public int WorkerId { get; set; }
    }
}
