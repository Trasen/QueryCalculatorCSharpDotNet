namespace QueryCalculator.Calculator.Calculations
{
    class Addition : Operator
    {
        public Addition() : base('+', 20)
        {
           
        }

        protected override decimal calculate(decimal num1, decimal num2)
        {
            return num1 + num2;
        }
    }
}