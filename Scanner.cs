namespace Swalang;

/// <summary>
/// This class is used to scan through the source code
/// adding tokens until it runs out of characters.
/// It appends an "end of file" (EOF) token at the end
/// <see cref="Token"/>
/// </summary>
public class Scanner
{
    /// <summary>
    /// This is a string representing the source code
    /// </summary>
    private readonly string _source;

    /// <summary>
    /// This is a list of tokens produced after scanning through
    /// the source code
    /// </summary>
    private readonly List<Token> _tokens = new();

    /// <summary>
    /// This points to the first character of the lexeme that is being scanned
    /// </summary>
    private int _start = 0;

    /// <summary>
    /// This points to the character currently being considered
    /// </summary>
    private int _current = 0;

    /// <summary>
    /// Tracks what source line current is on
    /// </summary>
    private int _line = 1;

    public Scanner(string source)
    {
        _source = source;
    }

    /// <summary>
    /// Scans through the source code one character at a time
    /// till the end
    /// </summary>
    /// <returns>A list of tokens <see cref="Token"/></returns>
    public List<Token> Scan()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }

        return _tokens;
    }

    /// <summary>
    /// Scans a token and adds the token to the list of tokens.
    /// For ever loop through the characters it advances the current
    /// pointer by 1
    /// </summary>
    private void ScanToken()
    {
        var c = Advance();
        switch(c)
        {
            case '(': AddToken(TokenType.L_PAREN); break;
            case ')': AddToken(TokenType.R_PAREN); break;
            case '{': AddToken(TokenType.L_CURLY); break;
            case '}': AddToken(TokenType.R_CURLY); break;
            case '[': AddToken(TokenType.R_SQUARE); break;
            case ']': AddToken(TokenType.R_SQUARE); break;
            case '.': AddToken(TokenType.DOT); break;
            case ',': AddToken(TokenType.COMMA); break;
            case '+': AddToken(TokenType.PLUS); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '*': AddToken(TokenType.STAR); break;
            case ';': AddToken(TokenType.SEMICOLON); break;
            default:
                Program.Error(_line, "Unexpected character.");
                break;
        }
    }

    /// <summary>
    /// Adds a token with a null string literal to the list of tokens
    /// </summary>
    /// <param name="type">The type of token <see cref="TokenType"/></param>
    private void AddToken(TokenType type)
    {
        AddToken(type, null);
    }

    /// <summary>
    /// Adds a new instance of a token to the list of tokens
    /// </summary>
    /// <param name="type">The type of token <see cref="TokenType"/></param>
    /// <param name="literal">
    /// The literal value of the token. The value is nullable
    /// </param>
    private void AddToken(TokenType type, string? literal)
    {
        var lexeme = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, lexeme, literal, _line));
    }

    /// <summary>
    /// Advances the current pointer by one
    /// </summary>
    /// <returns>The character at the previous index to the current index</returns>
    private char Advance()
    {
        _current += 1;
        return _source[_current - 1];
    }

    /// <summary>
    /// Checks to see if the current pointer is at the
    /// end of the source code
    /// </summary>
    /// <returns>True if current is at the end and False otherwise</returns>
    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }
}