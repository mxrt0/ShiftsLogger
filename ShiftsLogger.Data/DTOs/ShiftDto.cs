namespace ShiftsLogger.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    public TimeSpan Duration { get; set; }

    [Required]
    public int WorkerId { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(new string('-', 35));
        sb.AppendLine($"Start Date: {Date.ToString("dd-MM-yyyy")}\nStart Time: {Start.TimeOfDay.ToString("hh\\:mm")}\nEnd: {End.ToString("dd-MM-yyyy HH:mm")}");
        sb.AppendLine($"Duration: {Duration.ToString("hh\\:mm")}\nWorker ID: {WorkerId}");
        sb.AppendLine(new string('-', 35));
        return sb.ToString().TrimEnd();
    }
}
