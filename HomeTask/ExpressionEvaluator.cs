using System.Text.RegularExpressions;

namespace HomeTask;

public class ExpressionEvaluator : IExpressionEvaluator
{
    public string Evaluate(string expressionString)
    {
        string[] tokens = Regex.Split(expressionString, @"(\d+|[()*+x])").Where(i => !string.IsNullOrEmpty(i)).ToArray();
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer(tokens);
        var tree = lexicalAnalyzer.Scan();
        return tree.Evaluate().ToString()!;
    }
}