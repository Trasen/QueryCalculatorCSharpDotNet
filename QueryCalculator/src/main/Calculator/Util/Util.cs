using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator
{
    public class Util
    {
        public StringBuilder ReplacaseIndexFromTomInString(int start, int end, string original, string replacement)
        {
            List<char> array = new List<char>(original.ToCharArray());
            List<char> replacementArray = new List<char>(replacement.ToCharArray());

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