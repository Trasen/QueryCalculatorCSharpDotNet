using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : Calculator
    {
        private readonly Util _util = new Util();
        Dictionary<int, Dictionary<char, CalculationType>> orderOf = CalculationType.getOrderOf();
        List<int> keys;

        public CalculatorImpl()
        {
            keys = orderOf.Keys.ToList();
            keys.Sort();
        }

        public String calculate(String query)
        {
            try
            {
                query = removeUnsupportedCharacters(query);

                query = dealWithNestedCalculations(query);

                System.Console.WriteLine(query);
                return doCalculation(query);
            }
            catch (Exception e)
            {
                DataStore dataStore = DataStoreFactory.get();
                return e.ToString();
            }
        }

        private String removeUnsupportedCharacters(String query)
        {
            StringBuilder flushedString = new StringBuilder();

            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (CalculationType.Contains(ch) || char.IsDigit(ch) || ch == '(' || ch == ')' || ch == ',' ||
                    ch == '.')
                {
                    flushedString.Append(ch);
                }
            }


            query = flushedString.ToString().Replace(',', '.');
            return query;
        }

        private String dealWithNestedCalculations(String query)
        {
            NestedCalculationTracker tracker = NestedCalculationTracker.findNestedCalculation(query);

            while (tracker != null)
            {
                var calculation = NestedCalculationTracker.extractNestedCalculation(query, tracker);
                String result = doCalculation(calculation);

                StringBuilder stringBuilder = _util.replaceIndexFromTomInString(tracker.getIndexStart(),
                    tracker.getIndexEnd(), query, result);

                query = stringBuilder.ToString();

                tracker = NestedCalculationTracker.findNestedCalculation(query);
            }

            return query;
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

    public interface DataStore
    {
        String Save(Exception exception);
    }

    public class DataStoreFactory
    {
   

        public static DataStore get()
        {
            return new DefaultDatastore();
        }
    }

    public class DefaultDatastore : DataStore
    {
        public string Save(Exception exception)
        {
            Console.WriteLine(exception);
            return exception.ToString();
        }
    }
}