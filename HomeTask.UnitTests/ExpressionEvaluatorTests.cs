namespace HomeTask.UnitTests
{
    public class ExpressionEvaluatorTests
    {
        private IExpressionEvaluator _expressionEvaluator;

        [SetUp]
        public void Setup()
        {
            _expressionEvaluator = new ExpressionEvaluator();
        }

        [TestCase("x", ExpectedResult = "x")]
        [TestCase("(10+5)", ExpectedResult = "15")]
        [TestCase("1+2+3", ExpectedResult = "6")]
        [TestCase("(x+1)", ExpectedResult = "x+1")]
        [TestCase("1*x*2*x", ExpectedResult = "2*x^2")]
        [TestCase("1*x*2*x*3*x", ExpectedResult = "6*x^3")]
        [TestCase("((1+(2*x)+1)*(x+1))", ExpectedResult = "2*x^2+4*x+2")]
        [TestCase("(((1+x)*(1+x))+2)*(1+x)", ExpectedResult = "x^3+3*x^2+5*x+3")]
        public string MathEvaluatorTest(string expression)
        {
            return _expressionEvaluator.Evaluate(expression);
        }
    }
}