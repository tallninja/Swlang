namespace Swlang;

public abstract class ExpressionType
{
    public abstract T Accept<T>(IExpressionVisitor<T> visitor);
}

public class Binary : ExpressionType
{
    public Binary(ExpressionType leftOperand, Token @operator, ExpressionType rightOperand)
    {
        LeftOperand = leftOperand;
        Operator = @operator;
        RightOperand = rightOperand;
    }

    public ExpressionType LeftOperand { get; init; }
    public Token Operator { get; init; }
    public ExpressionType RightOperand { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Unary : ExpressionType
{
    public Unary(Token @operator, ExpressionType rightOperand)
    {
        Operator = @operator;
        RightOperand = rightOperand;
    }

    public Token Operator { get; init; }
    public ExpressionType RightOperand { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Grouping : ExpressionType
{
    public Grouping(ExpressionType expressionType)
    {
        Expression = expressionType;
    }

    public ExpressionType Expression { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class Literal : ExpressionType
{
    public Literal(object? value)
    {
        Value = value;
    }
    
    public object? Value { get; init; }

    public override T Accept<T>(IExpressionVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}