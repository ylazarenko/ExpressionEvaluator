namespace HomeTask;

public class AddExpression : IExpression
{
    public List<IExpression> Expressions { get; set; }

    public AddExpression(List<IExpression> expressions)
    {
        Expressions = expressions;
    }

    public IExpression Evaluate()
    {
        List<IExpression> evaluated = Expressions.Select(e => e.Evaluate()).ToList();
        List<IExpression> linearized = evaluated.SelectMany(e => e is AddExpression expression ? expression.Expressions : new List<IExpression> { e }).ToList();
        List<ElementExpression> elems = linearized.OfType<ElementExpression>().ToList();
        List<IExpression> others = linearized.Where(e => e is not ElementExpression).ToList();
        elems.Sort((e1, e2) => e1.Power.CompareTo(e2.Power));
        List<ElementExpression> added = new List<ElementExpression>();
        foreach (IGrouping<int, ElementExpression> group in elems.GroupBy(e => e.Power))
        {
            added.Add(new ElementExpression(group.Sum(e => e.Coefficient), group.Key));
        }
        List<IExpression> expressions = added.Concat(others).ToList();
        if (expressions.Count == 1)
        {
            return expressions[0];
        }

        return new AddExpression(expressions);
    }

    public IExpression Add(IExpression other)
    {
        return new AddExpression(Expressions.Concat(new List<IExpression> { other }).ToList());
    }

    public IExpression Multiply(IExpression other)
    {
        return new MultiplyExpression(new List<IExpression> {this, other});
    }

    public override string ToString()
    {
        return string.Join("+", Expressions.Select(e => e.ToString()).Reverse());
    }
}