namespace QueryCalculator.Calculator.Calculations
{
    class Addition : Operator
    {
        public Addition() : base('+', 20)
        {
           
        }

        protected override decimal calculate(params decimal[] values)
        {
            return values[0] + values[1];
        }
    }
}