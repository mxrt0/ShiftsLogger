using System.Globalization;

namespace ShiftsLogger.ConsoleUI.Utils
{
    public static class Validator
    {
        public static bool IsInputValid(string? input)
        {
            return !string.IsNullOrEmpty(input) && int.TryParse(input, out var result) && result >= 0
                && result <= Enum.GetValues(typeof(MenuOption)).Length - 1;
        }

        public static bool IsDateValid(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            input = input.Trim();

            return DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static bool IsStartTimeValid(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            input = input.Trim();

            return TimeSpan.TryParseExact(input, "HH\\:mm", CultureInfo.InvariantCulture, out _);
        }

        public static bool IsEndDateTimeValid(string? input, DateTime startDateTime)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (input.StartsWith("same", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(input, "same", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                {
                    return false;
                }

                return TimeSpan.TryParseExact(parts[1], "HH\\:mm", CultureInfo.InvariantCulture, out TimeSpan endTime)
                 && endTime > startDateTime.TimeOfDay;
            }
            return DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDateTime)
                && endDateTime > startDateTime;
        }

    }
}
