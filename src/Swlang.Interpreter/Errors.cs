namespace Swlang.Interpreter;

public class ParserError : Exception {}

public class RuntimeError : Exception
{
    public RuntimeError(Token token, string message) : base(message)
    {
        Token = token;
    }

    public Token Token { get; init; }
}
