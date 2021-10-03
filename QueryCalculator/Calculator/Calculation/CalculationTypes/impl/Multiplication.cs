namespace QueryCalculator.Calculator.Calculations
{
    class Multiplication : CalculationType
    {
        public Multiplication() : base('*')
        {
           
        }

        public override decimal calculate(decimal num1, decimal num2)
        {
            return num1 * num2;
        }
    }
}