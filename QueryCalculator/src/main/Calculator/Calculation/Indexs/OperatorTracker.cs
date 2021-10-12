namespace QueryCalculator.Calculator.Calculations
{
    public class OperatorTracker
    {
        protected int indexStart = -1;
        protected int indexEnd = -1;

        public OperatorTracker(int indexStart, int indexEnd) {
            this.indexStart = indexStart;
            this.indexEnd = indexEnd;
        }

        public OperatorTracker() {}

        public int getIndexStart() {
            return indexStart;
        }

        public int getIndexEnd() {
            return indexEnd;
        }
        
    }
}