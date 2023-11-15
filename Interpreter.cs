using CommandLine;
using static Swlang.TokenType;

namespace Swlang;

public class Interpreter : IExpressionVisitor<object?>, IStatementVisitor<object?>
{
    private readonly Variables _variables = new ();

    public void Interpret(List<StatementType> statements)
    {
        try
        {
            foreach (var statement in statements)
            {
                Execute(statement);
            }
        }
        catch (RuntimeError error)
        {
            Program.RuntimeError(error);
        }
    }

    public object? Visit(Binary expression)
    {
        var left = Evaluate(expression.LeftOperand);
        var @operator = expression.Operator;
        var right = Evaluate(expression.RightOperand);

        switch (@operator.Type)
        {
            case MINUS:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! - (double)right!;
            case STAR:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! * (double)right!;
            case F_SLASH:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! / (double)right!;
            case PLUS:
                switch (left)
                {
                    case double l when right is double r:
                        return l + r;
                    case string l when right is string r:
                        return l + r;
                }
                break;
            case GREATER:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! > (double)right!;
            case LESS:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! < (double)right!;
            case GREATER_EQUAL:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! >= (double)right!;
            case LESS_EQUAL:
                CheckNumberOperands(left!, @operator, right!);
                return (double)left! <= (double)right!;
            case EQUAL_EQUAL: return IsEqual(left, right);
            case BANG_EQUAL: return !IsEqual(left, right);
            default:
                return null;
        }

        return null;
    }

    public object? Visit(Unary expression)
    {
        var @operator = expression.Operator;
        var right = Evaluate(expression.RightOperand);
        CheckNumberOperand(@operator, right!);

        return @operator.Type switch
        {
            MINUS => -right.Cast<double>(),
            BANG => IsBoolean(right),
            _ => null
        };
    }

    public object? Visit(Variable expresion)
    {
        return _variables.Get(expresion.Name);
    }

    public object? Visit(Grouping expression)
    {
        return Evaluate(expression.Expression);
    }

    public object? Visit(Literal expression)
    {
        return expression.Value;
    }

    public object? Visit(ExpressionStatement statement)
    {
        Evaluate(statement.Expression);
        return null;
    }

    public object? Visit(PrintStatement statement)
    {
        var value = Evaluate(statement.Expression);
        Console.WriteLine(value);
        return null;
    }

    public object? Visit(VariableDeclarationStatement statement)
    {
        object? value = null;
        if (statement.Initializer is not null)
        {
            value = Evaluate(statement.Initializer);
        }
        _variables.Define(statement.Name.Lexeme, value!);
        return null;
    }

    private object? Evaluate(ExpressionType expression)
    {
        return expression.Accept(this);
    }

    private void Execute(StatementType statement)
    {
        statement.Accept(this);
    }

    private static bool IsBoolean(object? value)
    {
        return value switch
        {
            null => false,
            bool => value.Cast<bool>(),
            _ => true
        };
    }

    private static bool IsEqual(object? a, object? b)
    {
        return a switch
        {
            null when b is null => true,
            null => false,
            _ => a.Equals(b)
        };
    }

    private static void CheckNumberOperand(Token @operator, object operand)
    {
        if (operand is double) return;
        throw new RuntimeError(@operator, "Operand must be a number.");
    }

    private static void CheckNumberOperands(object left, Token @operator, object right)
    {
        if (left is double && right is double) return;
        throw new RuntimeError(@operator, "Operands must be a number");
    }
}