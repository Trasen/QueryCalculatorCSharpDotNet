using System;
using System.Text;
using System.Text.RegularExpressions;
using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : Calculator
    {
     public String calculate(String query) {

        query = removeUnsupportedCharacters(query);

        query = dealWithNestedCalculations(query);

        query = Regex.Replace(query, "[^0-9*-/+^]", "");

        return doCalculation(query);
}

        private String removeUnsupportedCharacters(String query) {
                query = Regex.Replace(query, "[^0-9*-/+^()]", "");
                return query;
        }

        private String dealWithNestedCalculations(String query) {
                OperatorTracker tracker;

                do {
                        tracker = findNestedCalculations(query);

                        if (tracker != null) {

                                String result = doCalculation(query.Substring(tracker.getIndexStart(), tracker.getIndexEnd()));

                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append(query);
                                stringBuilder.Remove(tracker.getIndexStart() -1, tracker.getIndexEnd()).Insert(tracker.getIndexStart() + 1, result);

                                query = stringBuilder.ToString();
                        }
                } while (tracker != null);
                return query;
        }

        private OperatorTracker findNestedCalculations(String query) {

                OperatorTracker tracker = new OperatorTracker();
                for (int i = 0; i < query.Length; i++) {

                        char ch = query[i];

                        if (ch == '(') {
                                tracker.setIndexStart(i + 1);
                        }

                        if (ch == ')') {
                                tracker.setIndexEnd(i);
                                return tracker;
                        }

                        if(i == query.Length-1 && tracker.getIndexEnd() == null) {
                                tracker.setIndexEnd(i);
                        }
                }
                return null;
        }

        private String doCalculation(String query) {

                Calculation calculation = new CalculationImpl(query);

                calculation.run(CalculationType.getTypeDynamically('^'));
                calculation.run(CalculationType.getTypeDynamically('*'));
                calculation.run(CalculationType.getTypeDynamically('+'));

                return calculation.getResult();
        }
    }
}