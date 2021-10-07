using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator.Calculations
{
    public abstract class Operator : CalculationType
    {
        public Operator(char mathOperator, int priority) : base(mathOperator, priority)
        {
        }

        override protected decimal[] extractCalculatableNumbers(List<IndexTracker> trackers, string query)
        {
            decimal[] numbers = new decimal[2];

            numbers[0] = Convert.ToDecimal(query.Substring(trackers[0].getIndexStart(),
                trackers[0].getIndexEnd() - trackers[0].getIndexStart()));
            numbers[1] = Convert.ToDecimal(query.Substring(trackers[1].getIndexStart(),
                trackers[1].getIndexEnd() - trackers[1].getIndexStart()));

            return numbers;
        }
    }
}