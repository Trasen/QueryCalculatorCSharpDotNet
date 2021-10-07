using System;
using System.Runtime.CompilerServices;

namespace QueryCalculator.Calculator.Calculations
{
   public class NestedCalculationTracker : Index
    {
        private const char open = '(';
        private const char close = ')';
        public NestedCalculationTracker(String query)
        {
            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (ch == open)
                {
                    start = i;
                }

                if (ch == close)
                {
                    end = i;
                    break;
                }
            }

        }
        
    }
}