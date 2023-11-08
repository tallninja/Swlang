using System.Text;
using static Swlang.Constants;

namespace Swlang;

internal abstract class Program
{
    private static bool _errorOccured;
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
                _log.Info(Usage);
                Environment.Exit(1);
                break;
        }
    }

    private static void RunPrompt()
    {
        while (true)
        {
            Console.Write(Prompt);
            var line = Console.In.ReadLine();
            if (line is null) break;
            Run(line);
            _errorOccured = false;
        }
    }

    private static void RunFile(string filePath)
    {
        try
        {
            using var sourceFile = File.Open(filePath, FileMode.Open);
            var streamReader = new StreamReader(sourceFile, Encoding.UTF8);
            Run(streamReader.ReadToEnd());

            if (_errorOccured)
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

    private static void Run(string sourceCode)
    {
        var lexer = new Lexer(sourceCode);
        var tokens = lexer.Scan();

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
        _errorOccured = true;
    }

}