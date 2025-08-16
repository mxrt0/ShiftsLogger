using ShiftsLogger.ConsoleUI.Utils;
using ShiftsLogger.Data.DTOs;
using System.Globalization;

namespace ShiftsLogger.ConsoleUI.Controllers
{
    public class UIController
    {
        private APIHelper _apiHelper;
        public UIController(APIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public async Task MainMenu()
        {
            Console.Clear();
            UIHelper.DisplayMenu();

            string? userInput = Console.ReadLine();

            while (!Validator.IsInputValid(userInput))
            {
                Console.WriteLine(Messages.InvalidInputMessage);
                userInput = Console.ReadLine();
            }
            await HandleUserInput((MenuOption)Enum.Parse(typeof(MenuOption), userInput!));
        }

        private async Task HandleUserInput(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.ExitApp:
                    Console.WriteLine("\nGoodbye...\n");
                    await Task.Delay(1000);
                    Environment.Exit(0);
                    break;
                case MenuOption.AddShift:
                    await AddShift();
                    break;
                case MenuOption.ViewShift:
                    //ViewShift();
                    break;
                case MenuOption.ViewAllShifts:
                    //ViewAllShifts();
                    break;
                case MenuOption.UpdateShift:
                    //UpdateShift();
                    break;
                case MenuOption.DeleteShift:
                    //DeleteShift();
                    break;
                case MenuOption.AddWorker:
                    //AddWorker();
                    break;
                case MenuOption.ViewAllWorkers:
                    //ViewAllWorkers();
                    break;
                case MenuOption.UpdateWorker:
                    //UpdateWorker();
                    break;
                case MenuOption.DeleteWorker:
                    //DeleteWorker();
                    break;
                default:
                    break;
            }
            await MainMenu();
        }

        private async Task AddShift()
        {
            Console.Clear();

            Console.WriteLine(Messages.ShiftStartDateMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime startDate = await GetShiftDateInput();

            Console.WriteLine(Messages.ShiftStartTimeMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime startTime = await GetShiftStartInput(startDate);

            Console.WriteLine(Messages.ShiftEndMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime endTime = await GetShiftEndInput(startDate);

            await DisplayWorkers();
            Console.WriteLine(Messages.ShiftWorkerMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerId = await GetWorkerInput();

            var newShift = new ShiftDto(startDate, startTime, endTime, workerId);
            string response = await _apiHelper.PostShiftAsync(newShift);
            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("There was unexpectedly no response from the server!");
                return;
            }
            Console.WriteLine(response);
        }

        private async Task CheckReturnToMainMenu(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            if (input == "0")
            {
                await MainMenu();
            }
        }

        private async Task<DateTime> GetShiftDateInput()
        {
            string? dateInput = Console.ReadLine();
            await CheckReturnToMainMenu(dateInput);

            while (!Validator.IsDateValid(dateInput))
            {
                Console.WriteLine(Messages.InvalidDateMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                dateInput = Console.ReadLine();
                await CheckReturnToMainMenu(dateInput);
            }
            return DateTime.ParseExact(dateInput!, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
        private async Task<DateTime> GetShiftStartInput(DateTime startDate)
        {
            string? startTimeInput = Console.ReadLine();
            await CheckReturnToMainMenu(startTimeInput);

            while (!Validator.IsStartTimeValid(startTimeInput))
            {
                Console.WriteLine(Messages.InvalidStartTimeMessage);
                startTimeInput = Console.ReadLine();
                await CheckReturnToMainMenu(startTimeInput);
            }
            return startDate.Date + TimeSpan.Parse(startTimeInput!);
        }
        private async Task<DateTime> GetShiftEndInput(DateTime startDateTime)
        {
            string? shiftEndInput = Console.ReadLine();
            await CheckReturnToMainMenu(shiftEndInput);

            while (!Validator.IsEndDateTimeValid(shiftEndInput, startDateTime))
            {
                Console.WriteLine(Messages.InvalidEndTimeMessage);
                shiftEndInput = Console.ReadLine();
                await CheckReturnToMainMenu(shiftEndInput);
            }
            if (shiftEndInput!.TrimStart().StartsWith("same", StringComparison.OrdinalIgnoreCase))
            {
                var parts = shiftEndInput.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2 && TimeSpan.TryParseExact(parts[1], "HH\\:mm", CultureInfo.InvariantCulture, out var time))
                {
                    return startDateTime.Date + time;
                }
            }

            return DateTime.ParseExact(shiftEndInput!, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        private async Task<int> GetWorkerInput()
        {
            string? workerNameInput = Console.ReadLine();
            await CheckReturnToMainMenu(workerNameInput);

            var workers = await _apiHelper.FetchWorkersAsync();
            while (string.IsNullOrEmpty(workerNameInput) || !workers.Any(w => w.Name == workerNameInput))
            {
                Console.WriteLine(Messages.InvalidWorkerMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                workerNameInput = Console.ReadLine();
                await CheckReturnToMainMenu(workerNameInput);
            }
            return workers.First(w => w.Name == workerNameInput).Id;
        }

        private async Task DisplayWorkers()
        {
            var workers = await _apiHelper.FetchWorkersAsync();
            Console.WriteLine(string.Join(Environment.NewLine, workers.Select(w => w.Name)));
            Console.WriteLine();
        }
    }
}
