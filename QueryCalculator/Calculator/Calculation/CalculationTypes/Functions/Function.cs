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
        
        public override String resolve(List<OperatorTracker> trackers, String query)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(query);

            decimal number1 = Convert.ToDecimal(query.Substring(trackers[0].getIndexStart(),
                trackers[0].getIndexEnd() - trackers[0].getIndexStart()));

            String tmp = Convert.ToString(calculate(number1));

            stringBuilder.Remove(trackers[0].getIndexStart(), trackers[1].getIndexEnd() - trackers[0].getIndexStart())
                .Insert(trackers[0].getIndexStart(), tmp);
            query = stringBuilder.ToString();
            Console.WriteLine(query);
            return query;
        }
    }
}