// ReSharper disable InconsistentNaming
namespace Swlang.Interpreter;

public enum TokenType
{
    // single character tokens
    L_PAREN, R_PAREN, L_CURLY, R_CURLY, L_SQUARE, R_SQUARE,
    COMMA, DOT, MINUS, PLUS, STAR, F_SLASH, B_SLASH, SEMICOLON,
    BANG, EQUAL, GREATER, LESS,

    //two character tokens
    BANG_EQUAL, LESS_EQUAL, GREATER_EQUAL, EQUAL_EQUAL,

    // literals
    STRING, NUMBER, IDENTIFIER,

    // keywords
    IF, WHILE, FOR, AND, OR, FUNC, CLASS, NULL, PRINT, RETURN,
    SUPER, THIS, TRUE, FALSE, VAR,

    EOF
}
