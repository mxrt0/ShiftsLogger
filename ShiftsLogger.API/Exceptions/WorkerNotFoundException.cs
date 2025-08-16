namespace ShiftsLogger.API.Exceptions;

public class WorkerNotFoundException : Exception
{
    public WorkerNotFoundException(string message) : base(message)
    {

    }
}
