using ShiftsLogger.ConsoleUI.Utils;

namespace ShiftsLogger.ConsoleUI.Controllers
{
    public class UIController
    {
        public UIController()
        {
            MainMenu();
        }
        private void MainMenu()
        {
            Console.Clear();
            UIHelper.DisplayMenu();

            string? userInput = Console.ReadLine();

            while (!Validator.IsInputValid(userInput))
            {
                Console.WriteLine(Messages.InvalidInputMessage);
                userInput = Console.ReadLine();
            }
        }
    }
}
