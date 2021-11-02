using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator.Calculations
{
    public abstract class Function : CalculationType
    {
        public Function(char mathOperator, int priority) : base(mathOperator, priority)
        {
        }

        override protected decimal[] extractCalculatableNumbers(List<OperatorTracker> trackers, string query)
        {
            var numbers = new decimal[1];

            numbers[0] = Convert.ToDecimal(query.Substring(trackers[1].getIndexStart(),
                trackers[1].getIndexEnd() - trackers[1].getIndexStart()));

            return numbers;
        }
    }
}