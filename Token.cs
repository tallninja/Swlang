namespace Swalang;


/// <summary>
/// This represents a token of information in the source code
/// <example>
/// Line: chombo a = "Jina";
/// Token: IDENTIFIER a
/// </example>
/// </summary>
public class Token
{
    private readonly TokenType _type;
    private readonly string _lexeme;
    private readonly string? _literal;
    private readonly int _line;

    public Token(TokenType type, string lexeme, string? literal, int line)
    {
        _type = type;
        _lexeme = lexeme;
        _literal = literal;
        _line = line;
    }

    /// <summary>
    /// This is the type of token extracted
    /// <see cref="TokenType"/>
    /// </summary>
    public TokenType Type => _type;

    /// <summary>
    /// This is a raw substring of the code
    /// </summary>
    public string Lexeme => _lexeme;

    /// <summary>
    /// This is the literal value of the token.
    /// For example when defining or assigning a value to a variable
    /// </summary>
    public string Literal => _literal ?? string.Empty;

    /// <summary>
    /// This is the line on the source file where the token is.
    /// This is useful especially in error reporting since it can be used
    /// to report the line where the error occured
    /// </summary>
    public int Line => _line;

    public override string ToString()
    {
        return $"{_type} {_lexeme} {_literal}";
    }
}