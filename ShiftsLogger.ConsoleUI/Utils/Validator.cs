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

            return TimeSpan.TryParseExact(input, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan time)
                && time > TimeSpan.Zero && time < TimeSpan.FromDays(1);
        }

        public static bool TryGetValidEndDateTime(string? input, DateTime startDateTime, out DateTime result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (input.StartsWith("same", StringComparison.OrdinalIgnoreCase))
            {
                var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    return false;

                if (!TimeSpan.TryParseExact(parts[1], "hh\\:mm", CultureInfo.InvariantCulture, out var time))
                    return false;

                result = startDateTime.Date + time;
                return result > startDateTime;
            }

            if (DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture,
                                       DateTimeStyles.None, out var endDateTime))
            {
                result = endDateTime;
                return result > startDateTime;
            }

            return false;
        }

    }
}
