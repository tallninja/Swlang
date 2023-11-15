using static Swlang.TokenType;

namespace Swlang;

public abstract class Constants
{
    public static string Prompt => "swalang >> ";
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