namespace Swlang;

public class Variables
{
    private readonly Dictionary<string, object> _variables = new();

    public void Define(string name, object value)
    {
        _variables.Add(name, value);
    }

    public object Get(Token name)
    {
        if (_variables.TryGetValue(name.Lexeme, out var variable)) return variable;
        throw new RuntimeError(name, $"Undefined variable {name.Lexeme}.");
    }
}