using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator
{
    public class Util
    {
        public StringBuilder ReplacaseIndexFromTomInString(int start, int end, string original, string replacement)
        {
            var array = new List<char>(original.ToCharArray());
            var replacementArray = new List<char>(replacement.ToCharArray());

            for (var i = start; i <= end; i++)
            {
                array.RemoveAt(start);
            }

            var nextIndex = start;
            for (var i = 0; i < replacement.Length; i++)
            {
                array.Insert(nextIndex, replacementArray[i]);
                nextIndex++;
            }


            return new StringBuilder(new string(array.ToArray()));
        }
    }
}