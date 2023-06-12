namespace HomeTask;

public class MultiplyExpression : IExpression
{
    public List<IExpression> Expressions { get; set; }

    public MultiplyExpression(List<IExpression> expressions)
    {
        Expressions = expressions;
    }

    public IExpression Evaluate()
    {
        List<IExpression> evaluated = Expressions.Select(e => e.Evaluate()).ToList();
        List<IExpression> linearized = evaluated.SelectMany(e => e is MultiplyExpression prod ? prod.Expressions : new List<IExpression> { e }).ToList();
        List<ElementExpression> elems = linearized.OfType<ElementExpression>().ToList();
        List<IExpression> sums = linearized.Where(e => e is not ElementExpression).ToList();
        var multipliedElements = new List<IExpression>
        {
            new ElementExpression(elems.Select(e => e.Coefficient).Aggregate(1, (a, b) => a * b), elems.Sum(e => e.Power))
        };
        foreach (IExpression s in sums)
        {
            List<IExpression> result = new List<IExpression>();
            foreach (var e1 in multipliedElements)
            {
                foreach (IExpression e2 in ((AddExpression)s).Expressions)
                {
                    result.Add(e1.Multiply(e2).Evaluate());
                }
            }
            multipliedElements = result;
        }

        return multipliedElements.Count == 1 ? multipliedElements[0] : new AddExpression(multipliedElements).Evaluate();
    }

    public IExpression Add(IExpression other)
    {
        return new AddExpression(new List<IExpression> { this, other });
    }

    public IExpression Multiply(IExpression other)
    {
        return new MultiplyExpression(Expressions.Concat(new List<IExpression> { other }).ToList());
    }

    public override string ToString()
    {
        return string.Join("*", Expressions.Select(e => e.ToString()));
    }
}