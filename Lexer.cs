namespace Swlang;
using static TokenType;
using static Constants;

/// <summary>
/// This class is used to scan through the source code
/// adding tokens until it runs out of characters.
/// It appends an "end of file" (EOF) token at the end
/// <see cref="Token"/>
/// </summary>
public class Lexer
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

    public Lexer(string source)
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
            case '[': AddToken(L_SQUARE); break;
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
            case '/':
                if (Match('/')) // ignore comments. proceed to the end of the comment
                    while (Peek() != '\n' && !IsAtEnd()) Advance();
                else
                    AddToken(F_SLASH);
                break;
            // ignore whitespace
            case ' ': case '\t': case '\r': break;
            case '\n': _line += 1; break;
            case '"': ParseStringToken(); break;
            default:
                if (IsDigit(c)) ParseNumberToken();
                if (IsAlpha(c)) ParseIdentifierToken();
                else Program.Error(_line, "Unexpected character.");
                break;
        }
    }

    /// <summary>
    /// Adds a new instance of a token to the list of tokens
    /// </summary>
    /// <param name="type">The type of token <see cref="TokenType"/></param>
    /// <param name="literal">
    /// The literal value of the token. The value is nullable
    /// </param>
    private void AddToken(TokenType type, object? literal = null)
    {
        var lexeme = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, lexeme, literal, _line));
    }

    /// <summary>
    /// Parses a string token. Starts token parsing after encountering
    /// the first " character. Throws an error if the string is unterminated.
    /// It also supports multiline strings
    /// </summary>
    private void ParseStringToken()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n')
                _line += 1;
            Advance();
        }

        if (IsAtEnd())
            Program.Error(_line, "Unterminated string.");

        // proceed to the closing "
        Advance();

        // remove the surrounding quotes
        var startIndex = _start + 1;
        var endIndex = _current - 1;
        var length = endIndex - startIndex;
        var literalValue = _source.Substring(startIndex, length);
        AddToken(STRING, literalValue);
    }

    /// <summary>
    /// Parses a numeric token.
    /// The token is parsed to a double by default
    /// </summary>
    private void ParseNumberToken()
    {
        while (IsDigit(Peek())) Advance();

        // look for a decimal
        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Advance();
            while (IsDigit(Peek())) Advance();
        }

        var numericValue = _source.Substring(_start, _current - _start);
        AddToken(NUMBER, double.Parse(numericValue));
    }

    /// <summary>
    /// Parses an identifier or a keyword token.
    /// For a token to pass as an identifier,
    /// it should start with an alphabetic letter or _.
    /// An identifier cannot start with a number or other characters
    /// </summary>
    private void ParseIdentifierToken()
    {
        while (IsAlphaNum(Peek())) Advance();

        var text = _source.Substring(_start, _current - _start);
        AddToken(Keywords.TryGetValue(text, out var value) ? value : IDENTIFIER);
    }

    /// <summary>
    /// Used to look/peek at the next character.
    /// </summary>
    /// <returns>The next character. If at the end returns null terminator</returns>
    private char Peek()
    {
        return IsAtEnd() ? '\0' : _source[_current];
    }

    /// <summary>
    /// Peeks at the next character after the current character pointer
    /// </summary>
    /// <returns>The character immediately after the current character</returns>
    private char PeekNext()
    {
        return _current + 1 > _source.Length ? '\0' : _source[_current + 1];
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
    /// <returns>True if current is at the end. False otherwise</returns>
    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }

    /// <summary>
    /// Checks if a character is a digit
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>True if is a digit. False otherwise</returns>
    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    /// <summary>
    /// Checks is a character is alphabetic
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>True if is alphabetic. False otherwise</returns>
    private static bool IsAlpha(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or '_';
    }

    /// <summary>
    /// Checks is a character is alphanumeric
    /// </summary>
    /// <param name="c">character</param>
    /// <returns>True if is alphanumeric. False otherwise</returns>
    private static bool IsAlphaNum(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }

}