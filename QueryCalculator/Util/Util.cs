﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator
{
    public class Util
    {
        public StringBuilder replaceIndexFromTomInString(int start, int end, String original, String replacement)
        {
            List<char> array = new(original.ToCharArray());
            List<char> replacementArray = new(replacement.ToCharArray());

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