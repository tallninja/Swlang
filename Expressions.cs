namespace Swlang;

public abstract class Expression
{
    public abstract T Accept<T>(IExpressionVisitor<T> visitor);
}

public class Binary : Expression
{
    public Binary(Expression leftOperand, Token @operator, Expression rightOperand)
    {
        LeftOperand = leftOperand;
        Operator = @operator;
        RightOperand = rightOperand;
    }

    public Expression LeftOperand { get; init; }
    public Token Operator { get; init; }
    public Expression RightOperand { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Unary : Expression
{
    public Unary(Token @operator, Expression expression)
    {
        Operator = @operator;
        Expression = expression;
    }

    public Token Operator { get; init; }
    public Expression Expression { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Literal : Expression
{
    public Literal(object value)
    {
        Value = value;
    }
    
    public object? Value { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Grouping : Expression
{
    public Grouping(Expression expression)
    {
        Expression = expression;
    }

    public Expression Expression { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}