namespace ShiftsLogger.API.Exceptions;

public class ShiftNotFoundException : Exception
{
    public ShiftNotFoundException(string message) : base(message)
    {
    }
}
