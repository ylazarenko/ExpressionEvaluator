namespace HomeTask;

public interface IExpression
{
    public IExpression Evaluate();
    public IExpression Add(IExpression other);
    public IExpression Multiply(IExpression other);
}