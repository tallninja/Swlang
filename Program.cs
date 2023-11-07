using System.Text;

namespace Swalang;

class Program
{
    public static bool ErrorOccured;
    private static Logger _log = new();

    public static void Main(string[] args)
    {
        switch (args.Length)
        {
            case 0:
                RunPrompt();
                break;
            case 1:
                RunFile(args[0]);
                break;
            default:
                _log.Info("Usage: swalang [script]");
                Environment.Exit(1);
                break;
        }
    }

    public static void RunPrompt()
    {
        while (true)
        {
            Console.Write(Constants.Prompt);
            var line = Console.In.ReadLine();
            if (line is null) break;
            Run(line);
            ErrorOccured = false;
        }
    }

    public static void RunFile(string filePath)
    {
        try
        {
            using var sourceFile = File.Open(filePath, FileMode.Open);
            var streamReader = new StreamReader(sourceFile, Encoding.UTF8);
            Run(streamReader.ReadToEnd());

            if (ErrorOccured)
            {
                Environment.Exit(65);
            }
        }
        catch (Exception exception)
        {
            _log.Error(exception.Message);
            Environment.Exit(65);
        }

    }

    public static void Run(string sourceCode)
    {
        var scanner = new Scanner(sourceCode);
        var tokens = scanner.Scan();

        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    public static void Error(int line, string message)
    {
        ReportError(line, string.Empty, message);
    }

    private static void ReportError(int line, string where, string message)
    {
        Console.Error.WriteLine($"[line {line}] Error {where} {message}");
        ErrorOccured = true;
    }

}