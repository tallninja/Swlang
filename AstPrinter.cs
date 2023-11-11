using System.Text;

namespace Swlang;

public class AstPrinter : IExpressionVisitor<string>
{
    public string Print(Expression expression)
    {
        return expression.Accept(this);
    }

    public string Visit(Binary expression)
    {
        return Parenthesize(
            expression.Operator.Lexeme,
            expression.LeftOperand,
            expression.RightOperand);
    }

    public string Visit(Unary expression)
    {
        return Parenthesize(expression.Operator.Lexeme, expression.Expression);
    }

    public string Visit(Literal expression)
    {
        return (expression.Value is null ? "tupu" : expression.Value.ToString()) ?? string.Empty;
    }

    public string Visit(Grouping expression)
    {
        return Parenthesize("group", expression.Expression);
    }

    private string Parenthesize(string name, params Expression[] expressions)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append('(');
        foreach (var expression in expressions)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append(expression.Accept(this));
        }
        stringBuilder.Append(')');

        return stringBuilder.ToString();
    }
}