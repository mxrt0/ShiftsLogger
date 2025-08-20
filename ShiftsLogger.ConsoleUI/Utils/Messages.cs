namespace ShiftsLogger.ConsoleUI.Utils
{
    public static class Messages
    {
        public static readonly string ReturnToMainMenuMessage = "\nType 0 to return to the Main Menu:\n";

        public static readonly string PressAnyKeyToContinueMessage = "\nPress any key to continue...\n";

        public static readonly string ActionQuestionMessage = "\nWhat you would like to do?";

        public static readonly string InvalidInputMessage = $"\nInvalid input. Enter a number between 0 and {Enum.GetValues(typeof(MenuOption)).Length - 1}!\n";

        public static readonly string ShiftStartDateMessage = "\nEnter the start date of the shift (Format: dd-MM-yyyy):\nEnter 'Today' for current date.\n";

        public static readonly string InvalidDateMessage = "\nInvalid date. Please try again! (Format: dd-MM-yyyy):\n";

        public static readonly string ShiftStartTimeMessage = "\nEnter the start time of the shift (24h clock -> Format HH:mm):\n";

        public static readonly string InvalidStartTimeMessage = "\nInvalid start time. Please try again! (24h clock -> Format HH:mm):\n";

        public static readonly string ShiftEndMessage = "\nEnter the end date and time of the shift (Format: dd-MM-yyyy HH:mm).\nReplace the date with 'same' to use the start date (e.g. 'same 01:30'):\n";

        public static readonly string ShiftWorkerPrompt = "\nChoose one of the available workers:\n";

        public static readonly string InvalidWorkerMessage = "\nInvalid input. Please enter the name of an existing available worker!\n";

        public static readonly string InvalidEndTimeMessage = "\nInvalid end time. Must be after the start time (Format: dd-MM-yyyy HH:mm or 'same HH:mm'):\n";

        public static readonly string WorkerNameMessage = "\nPlease enter the name of the worker (must not be duplicate to an already existing one):\n";

        public static readonly string InvalidWorkerNameMessage = "\nInvalid worker name. Please enter a non-null string containing at least 1 letter that is not duplicate to an existing worker name!\n";

        public static readonly string ShiftDoesNotExistMessage = "\nNo shift matching these details exists. Enter worker name again!\n";

    }
}
