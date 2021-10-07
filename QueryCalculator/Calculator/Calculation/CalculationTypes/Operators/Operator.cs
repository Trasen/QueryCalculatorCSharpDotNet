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

        override protected decimal[] extractCalculatableNumbers(List<Index> trackers, string query)
        {
            decimal[] numbers = new decimal[2];

            numbers[0] = Convert.ToDecimal(query.Substring(trackers[0].getStart(),
                trackers[0].getEnd() - trackers[0].getStart()));
            numbers[1] = Convert.ToDecimal(query.Substring(trackers[1].getStart(),
                trackers[1].getEnd() - trackers[1].getStart()));

            return numbers;
        }
    }
}