namespace QueryCalculator.Calculator.Calculations
{
    public class Subtraction : CalculationType
    {
        public Subtraction() : base('-', 20)
        {
        }

        public override decimal calculate(decimal num1, decimal num2)
        {
            return num1 - num2;
        }
    }
}