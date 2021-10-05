namespace QueryCalculator.Calculator.Calculations
{
    class Division : Operator
    {
        public Division() : base('/', 10)
        {
           
        }

        protected override decimal calculate(params decimal[] values)
        {
            return values[0] / values[1];
        }
    }
}