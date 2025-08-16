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
        }
    }
}
