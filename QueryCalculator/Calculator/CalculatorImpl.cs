using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : Calculator
    {
        private readonly Util _util = new();
        Dictionary<int, Dictionary<char, CalculationType>> orderOf = CalculationType.getOrderOf();
        List<int> keys;

        public CalculatorImpl()
        {
            keys = orderOf.Keys.ToList();
            keys.Sort();
        }

        public String calculate(String query)
        {
            query = removeUnsupportedCharacters(query);

            query = dealWithNestedCalculations(query);

            System.Console.WriteLine(query);
            return doCalculation(query);
        }

        private String removeUnsupportedCharacters(String query)
        {

            StringBuilder flushedString = new StringBuilder();
            
            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (CalculationType.Contains(ch) || char.IsDigit(ch) || ch == '(' || ch == ')' || ch == ',' || ch == '.')
                {
                    flushedString.Append(ch);
                }
            }


            query = flushedString.ToString().Replace(',', '.');
            return query;
        }

        private String dealWithNestedCalculations(String query)
        {
            OperatorTracker tracker;
            
            int i = 0;
            do
            {
                tracker = findNestedCalculations(query);

                if (tracker != null)
                {
                    String calculation = query.Substring(tracker.getIndexStart() + 1,
                        tracker.getIndexEnd() - tracker.getIndexStart() - 1);
                    String result = doCalculation(calculation);

                    StringBuilder stringBuilder = _util.replaceIndexFromTomInString(tracker.getIndexStart(),
                        tracker.getIndexEnd(), query, result);

                    query = stringBuilder.ToString();
                    i++;
                }
            } while (tracker != null);

            return query;
        }

        private OperatorTracker findNestedCalculations(String query)
        {
            OperatorTracker tracker = new OperatorTracker();
            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (ch == '(')
                {
                    tracker.setIndexStart(i);
                }

                if (ch == ')')
                {
                    tracker.setIndexEnd(i);
                    return tracker;
                }
            }

            return null;
        }

        private String doCalculation(String query)
        {
            Calculation calculation = new CalculationImpl(query);

            foreach (var key in keys)
            {
                calculation.run(CalculationType.getOrderOf()[key].Values.First());
            }
            
            return calculation.getResult();
        }
    }
}