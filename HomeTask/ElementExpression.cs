namespace HomeTask;

public class ElementExpression : IExpression
{
    public int Coefficient { get; set; }
    public int Power { get; set; }

    public ElementExpression(int coefficient = 1, int power = 1)
    {
        Coefficient = coefficient;
        Power = power;
    }

    public IExpression Evaluate()
    {
        return this;
    }

    public IExpression Add(IExpression other)
    {
        return new AddExpression(new List<IExpression> { this, other });
    }

    public IExpression Multiply(IExpression other)
    {
        return new MultiplyExpression(new List<IExpression> { this, other });
    }

    public override string ToString()
    {
        if (Power == 0)
        {
            return Coefficient.ToString();
        }

        var coefficient = Coefficient > 1 ? $"{Coefficient}*" : string.Empty;
        var power = Power > 1 ? $"^{Power}" : string.Empty;

        return $"{coefficient}x{power}";
    }
}