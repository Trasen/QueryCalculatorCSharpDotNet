using System.Dynamic;

namespace QueryCalculator.Calculator.Calculations
{
    public abstract class Index
    {
        protected int start = -1;
        protected int end = -1;

        public int getStart()
        {
            return start;
        }

        public int getEnd() {
            return end;
        }

        public bool IsComplete()
        {
            if (start == -1 || end == -1)
            {
                return false;
            }

            return true;
        }
    }
}