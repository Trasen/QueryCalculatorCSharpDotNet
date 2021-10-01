using System;
using System.Runtime.Versioning;
using CalculatorTests;

namespace QueryCalculator.Calculator
{

    class CalculationTracker
    {
        private int value1;
        private int value2;

        public CalculationTracker()
        {
            value1 = -1;
            value2 = -1;
        }

        public void setValue1(int value1)
        {
            this.value1 = value1;
        }

        public void setValue2(int value2)
        {
            this.value2 = value2;
        }

        public Boolean notSetAtAll()
        {
            if (this.value1 == -1 && this.value2 == -2)
            {
                return true;
            }

            return false;
        }

        public Boolean FirstValueSetButNotSecond()
        {
            if (value1 != -1 && value2 == -1)
            {
                throw new YouNeedToFinishNestedCalculationExpression();
            }

            return false;
        }

        public void notNestedCalculations(string query)
        {
            this.value1 = 0;
            this.value2 = query.Length;
        }

        public string getCalculation(string query)
        {

            if (value1 == -1)
            {
                return query;
            }

            return query.Substring(value1 + 1, value2 - 1);
        }
    }

    public class NestedCalculation
    {
        private string calculation;

        public NestedCalculation(string query)
        {

            if (query == null)
            {
                throw new NullReferenceException();
            }

            CalculationTracker calculationTracker = new CalculationTracker();

            for (int i = 0; i < query.Length; i++)
            {
                char character = query[i];

                if (character.Equals(')'))
                {
                    calculationTracker.setValue2(i);
                }

                if (character.Equals('('))
                {
                    calculationTracker.setValue1(i);
                }
                
            }

            calculationTracker.FirstValueSetButNotSecond();
            
            if (calculationTracker.notSetAtAll())
            {
                this.calculation = query;
                calculationTracker.notNestedCalculations(query);
            }

            else
            {
                calculation = calculationTracker.getCalculation(query);
            }

        }

        public string getCalcualtion()
        {
            return this.calculation;
        }
    }

    public class YouNeedToFinishNestedCalculationExpression : Exception
    {
    }
}