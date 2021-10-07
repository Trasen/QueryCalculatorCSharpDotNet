using System;
using System.Collections.Generic;
using System.Text;
using Index = QueryCalculator.Calculator.Calculations.Index;

namespace QueryCalculator.Calculator
{
    public class Util
    {
        public StringBuilder replaceIndexFromTomInString(Index index, String original, String replacement)
        {
            List<char> array = new(original.ToCharArray());
            List<char> replacementArray = new(replacement.ToCharArray());

            int start = index.getStart();
            int end = index.getEnd();
            
            for (int i = start; i <= end; i++)
            {
                array.RemoveAt(start);
            }

            int k = start;
            for (int i = 0; i < replacement.Length; i++)
            {
                array.Insert(k, replacementArray[i]);
                k++;
            }


            return new StringBuilder(new string(array.ToArray()));
        }
    }
}