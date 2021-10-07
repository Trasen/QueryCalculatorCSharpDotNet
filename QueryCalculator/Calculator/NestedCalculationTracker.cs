using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    internal class NestedCalculationTracker : OperatorTracker
    {
        public NestedCalculationTracker(string query)
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
    }
}