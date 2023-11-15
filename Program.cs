using System.Text;
using static Swlang.Constants;
using static Swlang.Utils;

namespace Swlang;

internal abstract class Program
{
    private static bool _errorOccured;
    private static bool _runtimeErrorOccurred;
    private static Logger _log = new();

    private static readonly Interpreter Interpreter = new();

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
        PrintAsciiArt();

        while (true)
        {
            ShowPrompt();
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

            if (_errorOccured) Environment.Exit(65);
            if (_runtimeErrorOccurred) Environment.Exit(72);
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

        // foreach (var token in tokens)
        // {
        //     Console.WriteLine(token);
        // }

        var parser = new Parser(tokens);
        var statements = parser.Parse();
        Interpreter.Interpret(statements);

        // if error occured stop execution
        if (_errorOccured) return;


    }

    public static void Error(int line, string message)
    {
        ReportError(line, string.Empty, message);
    }

    public static void Error(Token token, string message)
    {
        if (token.Type == TokenType.EOF) ReportError(token.Line, "mwishoni", message);
        else ReportError(token.Line,  $"katika '{token.Lexeme}'", message);
    }

    public static void RuntimeError(RuntimeError error)
    {
        Console.WriteLine($"{error.Message} \n[mstari {error.Token.Line}]");
        _runtimeErrorOccurred = true;
    }

    private static void ReportError(int line, string where, string message)
    {
        Console.Error.WriteLine($"[mstari {line}] kosa {where} {message}");
        _errorOccured = true;
    }

}