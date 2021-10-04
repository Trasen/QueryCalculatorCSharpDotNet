using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QueryCalculator.Calculator.Calculations
{
    public abstract class CalculationType
    {
        private static Dictionary<char, CalculationType> map = new();
        private static Dictionary<int, Dictionary<char, CalculationType>> priorityIndex = new();
        protected char mathOperator;
        protected int priority;

        static CalculationType() // Instance every CalculationType and put them in to order of priority statically - letting us simply create an implementation of a CalculationType and it will automatically be handled correctly in the solution.
        {
            Type type = typeof(CalculationType);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var loopType in types)
            {
                CalculationType calcType = (CalculationType) Activator.CreateInstance(loopType);
                map.TryAdd(calcType.mathOperator, calcType);
                
                priorityIndex.TryAdd(calcType.priority, new Dictionary<char, CalculationType>());
                priorityIndex[calcType.priority].Add(calcType.mathOperator, calcType);
                
            }
        }

        public CalculationType(char mathOperator, int priority)
        {
            this.mathOperator = mathOperator;
            this.priority = priority;
        }
        
        public bool isCharacterTheCurrentOperatorType(char currentCharacter)
        {
            return currentCharacter == this.getOperatorType();
        }

        public CalculationType samePriorityCalculations(char currentCharacter)
        {
            if(!this.isCharacterAnOperator(currentCharacter)) return this;
            
            Dictionary<char, CalculationType> samePriorityCalculations = CalculationType.getOrderOf()[this.getPriority()];

            try
            {
                return samePriorityCalculations[currentCharacter];
            }
            catch (Exception e)
            {
                return this;
            }
        }

        public static Dictionary<int, Dictionary<char, CalculationType>> getOrderOf()
        {
            return priorityIndex;
        }

        public int getPriority()
        {
            return this.priority;
        }

        public abstract decimal calculate(decimal num1, decimal num2);

        public bool isCharacterAnOperator(char mathOperator)
        {
            return map.ContainsKey(mathOperator);
        }

        public char getOperatorType()
        {
            return this.mathOperator;
        }
    }
}