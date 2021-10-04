using System;
using System.Text;
using System.Text.RegularExpressions;
using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : Calculator
    {
        private readonly Util _util = new Util();

        public String calculate(String query)
        {
            query = removeUnsupportedCharacters(query);

            query = dealWithNestedCalculations(query);

            query = Regex.Replace(query, "[^0-9*-/+^]", "");

            return doCalculation(query);
        }

        private String removeUnsupportedCharacters(String query)
        {
            query = Regex.Replace(query, "[^0-9*-/+^()]", "");
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
                    
                    StringBuilder stringBuilder = _util.replaceIndexFromTomInString(tracker.getIndexStart(), tracker.getIndexEnd(), query, result);
                    
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

                if (i == query.Length - 1 && tracker.getIndexEnd() == -1)
                {
                    tracker.setIndexEnd(i);
                }
            }

            return null;
        }

        private String doCalculation(String query)
        {
            Calculation calculation = new CalculationImpl(query);

            calculation.run(CalculationType.getTypeDynamically('^'));
            calculation.run(CalculationType.getTypeDynamically('*'));
            calculation.run(CalculationType.getTypeDynamically('+'));

            return calculation.getResult();
        }
    }
}