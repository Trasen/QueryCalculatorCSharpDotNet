﻿namespace QueryCalculator.Calculator.Calculations
{
    public class OperatorTracker
    {
        private int indexStart;
        private int indexEnd;

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

        public void setIndexStart(int indexStart) {
            this.indexStart = indexStart;
        }

        public void setIndexEnd(int indexEnd) {
            this.indexEnd = indexEnd;
        }
    }
}