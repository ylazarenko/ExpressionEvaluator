namespace HomeTask;

public class LexicalAnalyzer
{
    private readonly string[] _input;
    private int _index;

    public LexicalAnalyzer(string[] input)
    {
        _input = input;
        _index = 0;
    }

    public IExpression Scan(bool paren = false)
    {
        IExpression left = null!;
        while (_index < _input.Length)
        {
            string head = _input[_index];
            if (head == "(")
            {
                Next();
                left = Scan(true);
                continue;
            }
            if (head == ")")
            {
                if (paren)
                {
                    Next();
                }
                return left;
            }
            if (int.TryParse(head, out int coef))
            {
                Next();
                left = new ElementExpression { Coefficient = coef, Power = 0 };
                continue;
            }
            if (head == "x")
            {
                Next();
                left = new ElementExpression();
                continue;
            }
            if (head == "+")
            {
                Next();
                IExpression right = Scan();
                left = left.Add(right);
                continue;
            }
            if (head == "*")
            {
                Next();
                IExpression right = Scan();
                left = left.Multiply(right);
            }
        }

        return left;
    }

    private void Next()
    {
        _index++;
    }
}