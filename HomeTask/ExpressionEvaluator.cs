using System.Text.RegularExpressions;

namespace HomeTask;

public class ExpressionEvaluator : IExpressionEvaluator
{
    public string Evaluate(string expressionString)
    {
        string[] tokens = Regex.Matches(expressionString, @"\d+|[()*+x]")
            .Select(m => m.Value)
            .ToArray();
        IExpression tree = GetExpressionFromTokens(tokens).Item1;
        return tree.Evaluate().ToString()!;
    }

    private Tuple<IExpression, string[]> GetExpressionFromTokens(string[] tokens, bool parent = false)
    {
        IExpression left = null!;
        while (tokens.Length > 0)
        {
            if (tokens[0] == "(")
            {
                Tuple<IExpression, string[]> result = GetExpressionFromTokens(tokens.Skip(1).ToArray(), true);
                left = result.Item1;
                tokens = result.Item2;
                continue;
            }
            if (tokens[0] == ")")
            {
                return new Tuple<IExpression, string[]>(left!, parent ? tokens.Skip(1).ToArray() : tokens);
            }
            if (int.TryParse(tokens[0], out int num))
            {
                left = new ElementExpression(num, 0);
                tokens = tokens.Skip(1).ToArray();
                continue;
            }
            if (tokens[0] == "x")
            {
                left = new ElementExpression();
                tokens = tokens.Skip(1).ToArray();
                continue;
            }
            if (tokens[0] == "+")
            {
                Tuple<IExpression, string[]> result = GetExpressionFromTokens(tokens.Skip(1).ToArray());
                left = left!.Add(result.Item1);
                tokens = result.Item2;
                continue;
            }
            if (tokens[0] == "*")
            {
                Tuple<IExpression, string[]> result = GetExpressionFromTokens(tokens.Skip(1).ToArray());
                left = left!.Multiply(result.Item1);
                tokens = result.Item2;
                continue;
            }
        }

        return new Tuple<IExpression, string[]>(left!, tokens);
    }
}