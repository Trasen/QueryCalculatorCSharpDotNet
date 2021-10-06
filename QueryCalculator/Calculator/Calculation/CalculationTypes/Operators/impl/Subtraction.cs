using System;
using System.Collections.Generic;

namespace QueryCalculator.Calculator.Calculations
{
    public class Subtraction : Operator
    {
        public Subtraction() : base('-', 20)
        {
        }

        protected override decimal calculate(params decimal[] values)
        {
            return values[0] - values[1];
        }
        
    }
}