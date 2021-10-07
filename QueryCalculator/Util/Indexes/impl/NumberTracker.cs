using System;

namespace QueryCalculator.Calculator.Calculations
{
    class NumberTracker : Index
    {

        public NumberTracker(int lastOperatorIndex, int characterPositionInString)
        {
            start = lastOperatorIndex;
            end = characterPositionInString;
        }
        
    }
}