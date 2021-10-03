using System;

namespace QueryCalculator.Calculator.Calculations
{
    public class PowerOf : CalculationType
    {
        public PowerOf() : base('^')
        {
        }

        public override decimal calculate(decimal num1, decimal num2)
        {
            return Convert.ToDecimal(Math.Pow(Convert.ToDouble(num1), Convert.ToDouble(num2)));
        }
    }
}