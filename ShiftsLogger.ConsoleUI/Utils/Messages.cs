namespace ShiftsLogger.ConsoleUI.Utils
{
    public static class Messages
    {
        public static readonly string ActionQuestionMessage = "\nWhat you would like to do?";

        public static readonly string InvalidInputMessage = $"\nInvalid input. Please enter a number between 0 and {Enum.GetValues(typeof(MenuOption)).Length - 1}!\n";

    }
}
