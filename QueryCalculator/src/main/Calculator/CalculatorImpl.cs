using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using QueryCalculator.Calculator.Calculations;

namespace QueryCalculator.Calculator
{
    public class CalculatorImpl : ICalculator
    {
        private DataStore dataStore = DataStoreFactory.get();
        private readonly Util _util = new Util();
        private Dictionary<int, Dictionary<char, CalculationType>> orderOf = CalculationType.getOrderOf();
        private List<int> keys;

        public CalculatorImpl()
        {
            keys = orderOf.Keys.ToList();
            keys.Sort();
        }

        public String calculate(String query)
        {
            string originalQuery = query;
            try
            {
                query = removeUnsupportedCharacters(query);

                query = dealWithNestedCalculations(query);

                String result = doCalculation(query);
                dataStore.Save( "Original: " + originalQuery + "Result: " + result);
                return result;
            }
            catch (Exception e)
            {
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

                StringBuilder stringBuilder = _util.ReplacaseIndexFromTomInString(tracker.getIndexStart(),
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
        String Save(String value);
    }

    public class DataStoreFactory
    {

        private static DataStore dataStore;

        public DataStoreFactory(DataStore dataStoreInjection)
        {
            if (dataStore == null)
            {
                dataStore = dataStoreInjection;
            }
        }

        public static DataStore get()
        {
            if (dataStore == null)
                dataStore = new DefaultDatastore();

            return dataStore;
        }
    }

    public class DefaultDatastore : DataStore
    {
        public string Save(string value)
        {
            Console.WriteLine(value);
            return value;
        }
    }
}