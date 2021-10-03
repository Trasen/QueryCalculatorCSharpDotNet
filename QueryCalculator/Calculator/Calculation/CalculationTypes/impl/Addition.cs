namespace QueryCalculator.Calculator.Calculations
{
    class Addition : CalculationType
    {
        public Addition() : base('+')
        {
           
        }

        public override decimal calculate(decimal num1, decimal num2)
        {
            return num1 + num2;
        }
    }
}