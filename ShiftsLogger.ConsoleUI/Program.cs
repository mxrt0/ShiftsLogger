using ShiftsLogger.ConsoleUI.Controllers;
using ShiftsLogger.ConsoleUI.Utils;

namespace ShiftsLogger.ConsoleUI
{
    public class Program
    {
        static async Task Main()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7194") };
            var apiHelper = new APIHelper(httpClient);
            var controller = new UIController(apiHelper);
            await controller.MainMenu();
            httpClient.Dispose();
        }
    }
}
