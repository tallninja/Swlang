using static Swlang.TokenType;

namespace Swlang;

/// <summary>
/// The Parser is used to parse our tokens from the Lexer.
/// This is a recursive descent/top-down parser
/// The main function of the parser is to:
/// 1. Given a valid sequence of tokens, produce a corresponding syntax tree.
/// 2. Given an invalid sequence of tokens, detect any errors and tell the user about
/// their mistakes.
/// </summary>
public class Parser
{
    private readonly List<Token> _tokens;
    private int _current;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    /// <summary>
    /// expression -> equality
    /// </summary>
    /// <returns>Equality expression <see cref="Equality"/></returns>
    private Expression Expression()
    {
        return Equality();
    }

    /// <summary>
    /// equality -> comparison ( ( "==" | "!=" ) comparison )*
    /// </summary>
    /// <returns>Equality expression <see cref="Expression"/></returns>
    private Expression Equality()
    {
        var expression = Comparison();

        while (Match(BANG_EQUAL, EQUAL_EQUAL))
        {
            var @operator = Previous();
            var right = Comparison();
            expression = new Binary(expression, @operator, right);
        }

        return expression;
    }

    /// <summary>
    /// comparison -&gt; term ( ( "&lt;" | "&gt;" | "&lt;=" | "&gt;=" ) term )*
    /// </summary>
    /// <returns>Term expression <see cref="Term"/></returns>
    private Expression Comparison()
    {
        var expression = Term();

        while (Match(GREATER, GREATER_EQUAL, LESS, LESS_EQUAL))
        {
            var @operator = Previous();
            var right = Term();
            expression = new Binary(expression, @operator, right);
        }

        return expression;
    }

    /// <summary>
    /// term -&gt; factor ( ( "+" | "-" ) factor )*
    /// </summary>
    /// <returns>Factor expression <see cref="Factor"/></returns>
    private Expression Term()
    {
        var expression = Factor();

        while (Match(PLUS, MINUS))
        {
            var @operator = Previous();
            var right = Factor();
            expression = new Binary(expression, @operator, right);
        }

        return expression;
    }

    /// <summary>
    /// factor -&gt; unary ( ( "*" | "/" ) unary )*
    /// </summary>
    /// <returns>Unary expression <see cref="Unary"/></returns>
    private Expression Factor()
    {
        var expression = Unary();

        while (Match(F_SLASH, STAR))
        {
            var @operator = Previous();
            var right = Unary();
            expression = new Binary(expression, @operator, right);
        }

        return expression;
    }

    /// <summary>
    /// unary -&gt; ( "!" | "-" ) unary | primary
    /// </summary>
    /// <returns>
    /// Unary expression if the current token is ! or - <see cref="Unary"/>.
    /// Primary expression otherwise <see cref="Primary"/>
    /// </returns>
    private Expression Unary()
    {
        if (!Match(BANG, MINUS)) return Primary();
        var @operator = Previous();
        var right = Unary();
        return new Unary(@operator, right);

    }

    /// <summary>
    /// primary -&gt;  NUMBER | STRING | TRUE | FALSE | NULL | "(" expression ")"
    /// </summary>
    /// <returns>
    /// Literal expression <see cref="Literal"/>
    /// or Grouping expression <see cref="Grouping"/>
    /// </returns>
    private Expression? Primary()
    {
        if (Match(TRUE)) return new Literal(true);
        if (Match(FALSE)) return new Literal(false);
        if (Match(NULL)) return new Literal(null);
        if (Match(NUMBER, STRING)) return new Literal(Previous().Literal);

        // After we match an opening ( and parse the expression inside it,
        // we must find a ) token. If we don’t, that’s an error.
        if (Match(L_PAREN))
        {
            var expression = Expression();
            Consume(R_PAREN, "Expect ')' after expression.");
            return new Grouping(expression);
        }

        return null;
    }

    /// <summary>
    /// Consumes a token and throws an error if it encounters an invalid token
    /// </summary>
    /// <param name="type">The type of the token <see cref="TokenType"/></param>
    /// <param name="message">The error message</param>
    /// <returns></returns>
    /// <exception cref="ParserError"></exception>
    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw Error(Peek(), message);
    }

    /// <summary>
    /// Reports the error to the user
    /// </summary>
    /// <param name="token">Current token being parsed <see cref="Token"/></param>
    /// <param name="message">The error message</param>
    /// <returns></returns>
    private static ParserError Error(Token token, string message)
    {
        Program.Error(token, message);
        return new ParserError();
    }

    /// <summary>
    /// Checks to see if the current token has any of the given types
    /// </summary>
    /// <param name="tokenTypes">List of token types</param>
    /// <returns></returns>
    private bool Match(params TokenType[] tokenTypes)
    {
        if (!tokenTypes.Any(Check)) return false;
        Advance();

        return true;
    }

    /// <summary>
    /// Consumes the current token
    /// </summary>
    /// <returns>The current token <see cref="Token"/></returns>
    private Token Advance()
    {
        if (!IsAtEnd()) _current += 1;

        return Previous();
    }

    /// <summary>
    /// Checks if the current token is of the given type
    /// </summary>
    /// <param name="tokenType">Token Type <see cref="TokenType"/></param>
    /// <returns>boolean</returns>
    private bool Check(TokenType tokenType)
    {
        return !IsAtEnd() && Peek().Type == tokenType;
    }

    /// <summary>
    /// Returns the current token we have yet to consume
    /// </summary>
    /// <returns>Token <see cref="Token"/></returns>
    private Token Peek()
    {
        return _tokens[_current];
    }

    /// <summary>
    /// Returns the most recently consumed token
    /// </summary>
    /// <returns>Token <see cref="Token"/></returns>
    private Token Previous()
    {
        return _tokens[_current - 1];
    }

    /// <summary>
    /// Checks if we run out of tokens to parse
    /// </summary>
    /// <returns>True if current pointer is at the EOF token. False otherwise</returns>
    private bool IsAtEnd()
    {
        return Peek().Type == EOF;
    }
}

public class ParserError : ApplicationException {}