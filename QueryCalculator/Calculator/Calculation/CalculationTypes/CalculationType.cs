using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            if(!isCharacterAnOperator(currentCharacter)) return this;
            
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
        
        public static bool isCharacterAnOperator(char mathOperator)
        {
            return map.ContainsKey(mathOperator);
        }

        public char getOperatorType()
        {
            return this.mathOperator;
        }

        public static bool Contains(char ch)
        {
            return map.ContainsKey(ch);
        }
        
        protected string removeCalculatedExpressionFromQuery(List<Index> trackers, StringBuilder stringBuilder, string tmp)
        {
            string query;
            stringBuilder.Remove(trackers[0].getStart(), trackers[1].getEnd() - trackers[0].getStart())
                .Insert(trackers[0].getStart(), tmp);
            query = stringBuilder.ToString();
            Console.WriteLine(query);
            return query;
        }

        public String resolve(List<Index> trackers, String query)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(query);

            decimal[] numbers = extractCalculatableNumbers(trackers, query);
            
            String tmp = Convert.ToString(calculate(numbers));

            query = removeCalculatedExpressionFromQuery(trackers, stringBuilder, tmp);
            return query;
        }

        protected abstract decimal calculate(params decimal[] values);

        protected abstract decimal[] extractCalculatableNumbers(List<Index> trackers, string query);

    }
}