namespace ShiftsLogger.ConsoleUI.Utils
{
    public static class Validator
    {
        public static bool IsInputValid(string? input)
        {
            return !string.IsNullOrEmpty(input) && int.TryParse(input, out var result) && result >= 0
                && result <= Enum.GetValues(typeof(MenuOption)).Length - 1;
        }
    }
}
