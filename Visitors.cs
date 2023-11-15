namespace Swlang;

public interface IExpressionVisitor<T>
{
    T Visit(Binary expression);
    T Visit(Unary expression);
    T Visit(Literal expression);
    T Visit(Grouping expression);
}

public interface IStatementVisitor<T>
{
    T Visit(ExpressionStatement statement);
    T Visit(PrintStatement statement);
}