using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QueryCalculator.Calculator.Calculations;
using Index = QueryCalculator.Calculator.Calculations.Index;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : Calculator
    {
        private readonly Util _util = new();
        Dictionary<int, Dictionary<char, CalculationType>> orderOf = CalculationType.getOrderOf();
        List<int> keys;

        public CalculatorImpl()
        {
            keys = orderOf.Keys.ToList();
            keys.Sort();
        }

        public String calculate(String query)
        {
            query = removeUnsupportedCharacters(query);

            query = dealWithNestedCalculations(query);

            System.Console.WriteLine(query);
            return doCalculation(query);
        }

        private String removeUnsupportedCharacters(String query)
        {

            StringBuilder flushedString = new StringBuilder();
            
            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (CalculationType.Contains(ch) || char.IsDigit(ch) || ch == '(' || ch == ')' || ch == ',' || ch == '.')
                {
                    flushedString.Append(ch);
                }
            }


            query = flushedString.ToString().Replace(',', '.');
            return query;
        }

        private String dealWithNestedCalculations(String query)
        {
            Index index;
            
            int i = 0;
            do
            {
                index = new NestedCalculationTracker(query);
                String calculation = query.Substring(index.getStart() + 1,
                    index.getEnd() - index.getStart() - 1);
                String result = doCalculation(calculation);

                StringBuilder stringBuilder = _util.replaceIndexFromTomInString(index, query, result);

                query = stringBuilder.ToString();
                return query;
                
                    i++;
            } while (index != null);

            return query;
        }
        
        private Index findNestedCalculations(String query)
        {
            Index index = new NestedCalculationTracker(query);

            if (index.IsComplete())
            {
                return index;
            }

            return new Invalid();
        }

        private String doCalculation(String query)
        {
            Calculation calculation = new CalculationImpl(query);

            foreach (var key in keys)
            {
                calculation.run(CalculationType.getOrderOf()[key].Values.First());
            }
            
            return calculation.getResult();
        }
    }
}