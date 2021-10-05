using System;

namespace QueryCalculator.Calculator.Calculations
{
    class SquareRoot : Function
    {
        public SquareRoot() : base('√', 5)
        {
           
        }

        protected override decimal calculate(params decimal[] values)
        {
            return Convert.ToDecimal(Math.Sqrt(decimal.ToDouble(values[0])));
        }
    }
}