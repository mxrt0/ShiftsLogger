using System.Text;

namespace ShiftsLogger.Data.Entities;

public class Shift
{
    public Shift(DateTime date, DateTime start, DateTime end, int workerId)
    {
        Date = date;
        Start = start;
        End = end;
        WorkerId = workerId;
        CalculateDuration();
    }

    public Shift()
    {

    }

    public int Id { get; set; }

    public DateTime Date { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
    public TimeSpan Duration { get; set; }

    public int WorkerId { get; set; }
    public Worker Worker { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(new string('-', 35));
        sb.AppendLine($"ID: {Id}\nDate: {Date.ToShortDateString()}\nDuration: {Duration.ToString("hh\\:mm")} WorkerID: {WorkerId}");
        sb.AppendLine(new string('-', 35));
        return sb.ToString().TrimEnd();
    }

    public void CalculateDuration() => Duration = End - Start;

}
