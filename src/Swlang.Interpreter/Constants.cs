using static Swlang.Interpreter.TokenType;

namespace Swlang.Interpreter;

public abstract class Constants
{
    public static string Prompt => "swlang >> ";
    public static string Usage => "Usage: swalang [script]";

    public static Dictionary<string, TokenType> Keywords => new()
    {
        { "chombo", VAR },
        { "ikiwa", IF },
        { "mradi", WHILE },
        { "kwakila", FOR },
        { "ukweli", TRUE },
        { "uwongo", FALSE },
        { "na", AND },
        { "ama", OR },
        { "kitendo", FUNC },
        { "ramani", CLASS },
        { "tupu", NULL },
        { "sema", PRINT },
        { "rudisha", RETURN },
        { "mkuu", SUPER },
        { "hiki", THIS }
    };
}
