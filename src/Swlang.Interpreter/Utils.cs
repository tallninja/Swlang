using static Swlang.Interpreter.Constants;

namespace Swlang.Interpreter;

public abstract class Utils
{
    private const string _asciiArt =
        "\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2588\u2588\u2557    \u2588\u2588\u2557\u2588\u2588\u2557      \u2588\u2588\u2588\u2588\u2588\u2557 \u2588\u2588\u2588\u2557   \u2588\u2588\u2557 \u2588\u2588\u2588\u2588\u2588\u2588\u2557 \n\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d\u2588\u2588\u2551    \u2588\u2588\u2551\u2588\u2588\u2551     \u2588\u2588\u2554\u2550\u2550\u2588\u2588\u2557\u2588\u2588\u2588\u2588\u2557  \u2588\u2588\u2551\u2588\u2588\u2554\u2550\u2550\u2550\u2550\u255d \n\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2588\u2588\u2551 \u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2551     \u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2551\u2588\u2588\u2554\u2588\u2588\u2557 \u2588\u2588\u2551\u2588\u2588\u2551  \u2588\u2588\u2588\u2557\n\u255a\u2550\u2550\u2550\u2550\u2588\u2588\u2551\u2588\u2588\u2551\u2588\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2551     \u2588\u2588\u2554\u2550\u2550\u2588\u2588\u2551\u2588\u2588\u2551\u255a\u2588\u2588\u2557\u2588\u2588\u2551\u2588\u2588\u2551   \u2588\u2588\u2551\n\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2551\u255a\u2588\u2588\u2588\u2554\u2588\u2588\u2588\u2554\u255d\u2588\u2588\u2588\u2588\u2588\u2588\u2588\u2557\u2588\u2588\u2551  \u2588\u2588\u2551\u2588\u2588\u2551 \u255a\u2588\u2588\u2588\u2588\u2551\u255a\u2588\u2588\u2588\u2588\u2588\u2588\u2554\u255d\n\u255a\u2550\u2550\u2550\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u255d\u255a\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u2550\u255d\u255a\u2550\u255d  \u255a\u2550\u255d\u255a\u2550\u255d  \u255a\u2550\u2550\u2550\u255d \u255a\u2550\u2550\u2550\u2550\u2550\u255d \n";

    public static void PrintAsciiArt()
    {
        Console.Clear(); // clear the console

        // Calculate left margin for centering
        var leftMargin = (Console.WindowWidth - _asciiArt.Split('\n')[0].Length) / 2;

        // Print the ASCII art with the calculated left margin
        Console.Clear(); // Optional: Clear the console before printing
        Console.WriteLine();
        PrintCenteredAsciiArt(_asciiArt, leftMargin);
        Console.WriteLine();
    }

    public static void ShowPrompt()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(Prompt);
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static void PrintCenteredAsciiArt(string asciiArt, int leftMargin)
    {
        foreach (var line in asciiArt.Split('\n'))
        {
            Console.SetCursorPosition(leftMargin, Console.CursorTop);
            Console.WriteLine(line);
        }
    }
}
