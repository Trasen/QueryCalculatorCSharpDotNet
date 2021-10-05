using System;

namespace QueryCalculator.Calculator.Calculations
{
    public class PowerOf : Operator
    {
        public PowerOf() : base('^', 5)
        {
        }

        protected override decimal calculate(params decimal[] values)
        {
            return Convert.ToDecimal(Math.Pow(Convert.ToDouble(values[0] ), Convert.ToDouble(values[1])));
        }
        
    }
}