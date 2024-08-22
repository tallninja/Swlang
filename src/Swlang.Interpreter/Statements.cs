namespace Swlang.Interpreter;

public abstract class StatementType
{
    public abstract T Accept<T>(IStatementVisitor<T> visitor);
}

public class ExpressionStatement : StatementType
{
    public ExpressionStatement(ExpressionType expression)
    {
        Expression = expression;
    }

    public ExpressionType Expression { get; init; }

    public override T Accept<T>(IStatementVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class PrintStatement : StatementType
{
    public PrintStatement(ExpressionType expression)
    {
        Expression = expression;
    }

    public ExpressionType Expression { get; init; }

    public override T Accept<T>(IStatementVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class VariableDeclarationStatement : StatementType
{
    public VariableDeclarationStatement(Token name, ExpressionType? initializer)
    {
        Name = name;
        Initializer = initializer;
    }

    public Token Name { get; init; }
    public ExpressionType? Initializer { get; init; }
    public override T Accept<T>(IStatementVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}

public class AssignmentStatement : StatementType
{
    public AssignmentStatement(Token name, ExpressionType value)
    {
        Name = name;
        Value = value;
    }

    public Token Name { get; init; }
    public ExpressionType Value { get; init; }
    public override T Accept<T>(IStatementVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
