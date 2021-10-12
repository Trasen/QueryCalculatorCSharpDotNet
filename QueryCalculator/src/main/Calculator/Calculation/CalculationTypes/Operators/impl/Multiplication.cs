namespace QueryCalculator.Calculator.Calculations
{
    class Multiplication : Operator
    {
        public Multiplication() : base('*', 10)
        {
           
        }

        protected override decimal calculate(params decimal[] values)
        {
            return values[0] * values[1];
        }
    }
}