namespace Swlang;

public class Logger
{
    public void Debug(string message)
    {
        Console.WriteLine($"[DEBUG] {message}");
    }

    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    public void Warn(string message)
    {
        Console.WriteLine($"[WARN] {message}");
    }

    public void Error(string message)
    {
        Console.Error.WriteLine($"[ERROR] {message}");
    }

    public void Fatal(string message)
    {
        Console.Error.WriteLine($"[FATAL] {message}");
    }
}