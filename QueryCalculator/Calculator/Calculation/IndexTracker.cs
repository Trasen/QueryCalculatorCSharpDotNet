namespace QueryCalculator.Calculator.Calculations
{
    public class IndexTracker
    {
        private int indexStart = -1;
        private int indexEnd = -1;

        public IndexTracker(int indexStart, int indexEnd) {
            this.indexStart = indexStart;
            this.indexEnd = indexEnd;
        }

        public IndexTracker() {}

        public int getIndexStart() {
            return indexStart;
        }

        public int getIndexEnd() {
            return indexEnd;
        }

        public void setIndexStart(int indexStart) {
            this.indexStart = indexStart;
        }

        public void setIndexEnd(int indexEnd) {
            this.indexEnd = indexEnd;
        }
    }
}