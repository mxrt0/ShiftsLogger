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
                    await ViewShift();
                    break;
                case MenuOption.ViewAllShifts:
                    await ViewAllShifts();
                    break;
                case MenuOption.UpdateShift:
                    await UpdateShift();
                    break;
                case MenuOption.DeleteShift:
                    await DeleteShift();
                    break;
                case MenuOption.AddWorker:
                    await AddWorker();
                    break;
                case MenuOption.ViewAllWorkers:
                    await ViewAllWorkers();
                    break;
                case MenuOption.UpdateWorker:
                    await UpdateWorker();
                    break;
                case MenuOption.DeleteWorker:
                    await DeleteWorker();
                    break;
                default:
                    break;
            }
            await MainMenu();
        }

        private async Task DeleteWorker()
        {
            Console.Clear();
            await DisplayWorkers();

            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerToDeleteId = await GetWorkerInput();

            Console.WriteLine("\nFound worker! Are you sure you wish to delete them [Y/N]:\n");
            string? confirm = Console.ReadLine();
            while (string.IsNullOrEmpty(confirm) || (confirm.ToLower() != "y" && confirm.ToLower() != "n"))
            {
                Console.WriteLine("\nInvalid input. Please enter either Y or N:\n");
                confirm = Console.ReadLine();
            }
            if (confirm.ToLower() == "n")
            {
                return;
            }
            string response = await _apiHelper.DeleteWorker(workerToDeleteId);
            PrintResponse(response);
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task DeleteShift()
        {
            Console.Clear();
            Console.WriteLine("\nEnter the details of the shift you wish to delete:\n");
            await DisplayWorkers();
            var shiftToDelete = GetExistingShift();

            Console.WriteLine("\nFound shift! Are you sure you wish to delete it [Y/N]:\n");
            string? confirm = Console.ReadLine();
            while (string.IsNullOrEmpty(confirm) || (confirm.ToLower() != "y" && confirm.ToLower() != "n"))
            {
                Console.WriteLine("\nInvalid input. Please enter either Y or N:\n");
                confirm = Console.ReadLine();
            }
            if (confirm.ToLower() == "n")
            {
                return;
            }
            string response = await _apiHelper.DeleteShift(shiftToDelete.Id);
            PrintResponse(response);
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task UpdateWorker()
        {
            Console.Clear();

            await DisplayWorkers();
            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerToUpdateId = await GetWorkerInput();

            Console.WriteLine("\nFound worker! Enter new name (must not be duplicate to any other worker):\n");
            string newName = await GetWorkerNameInput();

            var newWorker = new CreateWorkerDto(newName);
            string response = await _apiHelper.UpdateWorker(workerToUpdateId, newWorker);
            PrintResponse(response);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task UpdateShift()
        {
            Console.Clear();

            await DisplayWorkers();
            Console.WriteLine("\nEnter the details of the shift you wish to update:\n");
            var shiftToUpdate = GetExistingShift();
            Console.WriteLine("\nFound shift! Now enter the new details:\n");

            Console.WriteLine(Messages.ShiftStartDateMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime newStartDate = await GetShiftDateInput();

            Console.WriteLine(Messages.ShiftStartTimeMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime newStartTime = await GetShiftStartInput(newStartDate);

            Console.WriteLine(Messages.ShiftEndMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            DateTime newEndTime = await GetShiftEndInput(newStartTime);

            await DisplayWorkers();
            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int newWorkerId = await GetWorkerInput();

            var updatedShift = new ShiftDto(newStartDate, newStartTime, newEndTime, newWorkerId);
            string response = await _apiHelper.UpdateShift(shiftToUpdate.Id, updatedShift);

            PrintResponse(response);
            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task ViewAllShifts()
        {
            Console.Clear();

            (string Response, List<ShiftDto>? Shifts) result = await _apiHelper.GetAllShiftsAsync();
            if (result.Shifts is null)
            {
                PrintResponse(result.Response);
            }
            else
            {
                Console.WriteLine(string.Join(Environment.NewLine, result.Shifts));
            }

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task ViewAllWorkers()
        {
            Console.Clear();
            await DisplayWorkers();

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task ViewShift()
        {
            Console.Clear();

            await DisplayWorkers();
            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerId = await GetWorkerInput();

            Console.WriteLine(Messages.ShiftStartDateMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var startDate = await GetShiftDateInput();

            (string Response, ShiftDto? Shift) result = await _apiHelper.GetShiftAsync(workerId, startDate);
            if (result.Shift is not null)
            {
                Console.WriteLine(Environment.NewLine + result.Shift);
            }
            else
            {
                PrintResponse(result.Response);
            }

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
        }

        private async Task AddWorker()
        {
            Console.Clear();

            Console.WriteLine(Messages.WorkerNameMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            string workerName = await GetWorkerNameInput();

            var newWorker = new CreateWorkerDto(workerName);
            string response = await _apiHelper.PostWorkerAsync(newWorker);
            PrintResponse(response);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
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
            DateTime endTime = await GetShiftEndInput(startTime);

            await DisplayWorkers();
            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerId = await GetWorkerInput();

            var newShift = new ShiftDto(startDate, startTime, endTime, workerId);
            string response = await _apiHelper.PostShiftAsync(newShift);
            PrintResponse(response);

            Console.WriteLine(Messages.PressAnyKeyToContinueMessage);
            Console.ReadKey();
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
            if (!string.IsNullOrEmpty(dateInput) && string.Equals(dateInput.Trim(), "today", StringComparison.OrdinalIgnoreCase))
            {
                return DateTime.Today;
            }
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

            DateTime result;
            while (!Validator.TryGetValidEndDateTime(shiftEndInput, startDateTime, out result))
            {
                Console.WriteLine(Messages.InvalidEndTimeMessage);
                shiftEndInput = Console.ReadLine();
                await CheckReturnToMainMenu(shiftEndInput);
            }

            return result;
        }

        private async Task<int> GetWorkerInput()
        {
            string? workerNameInput = Console.ReadLine();
            await CheckReturnToMainMenu(workerNameInput);

            var workers = await _apiHelper.FetchWorkersAsync();
            while (string.IsNullOrEmpty(workerNameInput) || !workers.Any(w => string.Equals(w.Name, workerNameInput, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.InvalidWorkerMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                workerNameInput = Console.ReadLine();
                await CheckReturnToMainMenu(workerNameInput);
            }
            return workers.First(w => string.Equals(w.Name, workerNameInput, StringComparison.OrdinalIgnoreCase)).Id;
        }

        private async Task<string> GetWorkerNameInput()
        {
            string? workerNameInput = Console.ReadLine();
            await CheckReturnToMainMenu(workerNameInput);

            var workers = await _apiHelper.FetchWorkersAsync();
            while (string.IsNullOrEmpty(workerNameInput) || workers.Any(w => string.Equals(w.Name, workerNameInput, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(Messages.InvalidWorkerNameMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                workerNameInput = Console.ReadLine();
                await CheckReturnToMainMenu(workerNameInput);
            }
            return workerNameInput;
        }

        private async Task DisplayWorkers()
        {
            try
            {
                var workers = await _apiHelper.FetchWorkersAsync();
                Console.WriteLine("\nAvailable workers:" + Environment.NewLine + string.Join(Environment.NewLine, workers.Select(w => w.Name)));
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async Task<ShiftDto> GetExistingShift()
        {
            Console.WriteLine(Messages.ShiftWorkerPrompt);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            int workerId = await GetWorkerInput();

            Console.WriteLine(Messages.ShiftStartDateMessage);
            Console.WriteLine(Messages.ReturnToMainMenuMessage);
            var startDate = await GetShiftDateInput();

            (string _, ShiftDto? Shift) result = await _apiHelper.GetShiftAsync(workerId, startDate);
            while (result.Shift is null)
            {
                Console.WriteLine(Messages.ShiftDoesNotExistMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                workerId = await GetWorkerInput();

                Console.WriteLine(Messages.ShiftStartDateMessage);
                Console.WriteLine(Messages.ReturnToMainMenuMessage);
                startDate = await GetShiftDateInput();

                result = await _apiHelper.GetShiftAsync(workerId, startDate);
            }
            return result.Shift;
        }

        private void PrintResponse(string? response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Console.WriteLine("\nThere was unexpectedly no response from the server!\n");
                return;
            }
            Console.WriteLine(Environment.NewLine + response);
        }
    }
}
