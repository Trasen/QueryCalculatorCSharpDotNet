﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QueryCalculator.Calculator.Calculations
{

    public abstract class CalculationType
    {
        private static Dictionary<char, CalculationType> map = new();
        protected char mathOperator;

        static CalculationType()
        {

            var type = typeof(CalculationType);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var loopType in types)
            {
                CalculationType calcType = (CalculationType) Activator.CreateInstance(loopType);
                map.TryAdd(calcType.mathOperator, calcType);
            }
            
        }

        public CalculationType(char mathOperator)
        {
            this.mathOperator = mathOperator;
        }

        public abstract decimal calculate(decimal num1, decimal num2);

        public static bool isCharacterAnOperator(char mathOperator)
        {
            return map.ContainsKey(mathOperator);
        }

        public char getOperatorType()
        {
            return this.mathOperator;
        }

        public static CalculationType getTypeDynamically(char mathOperator)
        {
            return map[mathOperator];
        }
    }
}