using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    internal class NestedCalculationTracker : OperatorTracker
    {
        private NestedCalculationTracker(string query)
        {
            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (ch == '(')
                {
                    indexStart = i;
                }

                if (ch == ')')
                {
                    indexEnd = i;
                    break;
                }
            }
        }

        public bool IsComplete()
        {
            return !(indexStart == -1 || indexEnd == -1);;
        }

        public static NestedCalculationTracker findNestedCalculation(string query)
        {
            NestedCalculationTracker tracker = new NestedCalculationTracker(query);

            if (tracker.IsComplete())
            {
                return tracker;
            }

            return null;
        }
        
        public static string extractNestedCalculation(string query, OperatorTracker tracker)
        {
            string calculation = query.Substring(tracker.getIndexStart() + 1,
                tracker.getIndexEnd() - tracker.getIndexStart() - 1);
            return calculation;
        }
    }
}