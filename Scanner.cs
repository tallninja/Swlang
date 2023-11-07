namespace Swalang;
using static TokenType;

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
            case '(': AddToken(L_PAREN); break;
            case ')': AddToken(R_PAREN); break;
            case '{': AddToken(L_CURLY); break;
            case '}': AddToken(R_CURLY); break;
            case '[': AddToken(R_SQUARE); break;
            case ']': AddToken(R_SQUARE); break;
            case '.': AddToken(DOT); break;
            case ',': AddToken(COMMA); break;
            case '+': AddToken(PLUS); break;
            case '-': AddToken(MINUS); break;
            case '*': AddToken(STAR); break;
            case ';': AddToken(SEMICOLON); break;
            case '=':
                AddToken(Match('=') ? EQUAL_EQUAL : EQUAL);
                break;
            case '!':
                AddToken(Match('=') ? BANG_EQUAL : BANG);
                break;
            case '>':
                AddToken(Match('=') ? GREATER_EQUAL : GREATER);
                break;
            case '<':
                AddToken(Match('=') ? LESS_EQUAL : LESS);
                break;
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
    /// Used to scan double character tokens.
    /// if the next character matches then it increments the current pointer
    /// </summary>
    /// <param name="expected">The expected character</param>
    /// <returns>
    /// True if current character matches the expected character.
    /// Returns False otherwise
    /// </returns>
    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (_source[_current] != expected) return false;
        _current += 1;
        return true;
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