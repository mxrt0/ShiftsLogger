using System.Text.RegularExpressions;
namespace ShiftsLogger.ConsoleUI.Utils;

public static class UIHelper
{
    public static void DisplayMenu()
    {
        Console.WriteLine("\n--Shifts Logger--\n\nMAIN MENU\n\n- - - - - - - - - - - - - -");
        Console.WriteLine(Messages.ActionQuestionMessage);

        foreach (var option in Enum.GetValues(typeof(MenuOption)))
        {
            Console.WriteLine($"\nType {(int)option} to {Regex.Replace(Enum.GetName((MenuOption)option)!, "([A-Z])", " $1")}");
        }
        Console.WriteLine();
        Console.Write("Your input: ");
    }


}
