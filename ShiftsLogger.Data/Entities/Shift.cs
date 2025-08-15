namespace ShiftsLogger.Data.Entities
{
    public class Shift
    {
        public Shift(DateTime date, DateTime start, DateTime end)
        {
            Date = date;
            Start = start;
            End = end;
            Duration = Start - End;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
