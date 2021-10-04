using System;
using System.Collections.Generic;
using System.Text;

namespace QueryCalculator.Calculator.Calculations
{
    class CalculationImpl : Calculation
    {
        private String query;
        public CalculationImpl(String query)
        {
            this.query = query;
        }

        public Calculation run(CalculationType type)
        {
            int lastOperatorIndex = 0;
            List<OperatorTracker> operatorTrackers = new();

            for (int characterPositionInString = 0;
                characterPositionInString < query.Length;
                characterPositionInString++)
            {
                char currentCharacter = query[characterPositionInString];

                type = samePriorityCalculations(currentCharacter, type);

                if (isCharacterTheCurrentOperatorType(currentCharacter, type))
                {
                    operatorTrackers.Add(new OperatorTracker(lastOperatorIndex, characterPositionInString));

                    lastOperatorIndex = characterPositionInString + 1;

                    for (int j = characterPositionInString + 1; j < query.Length; j++)
                    {
                        char nestedCharacter = query[j];

                        if (CalculationType.isCharacterAnOperator(nestedCharacter))
                        {
                            operatorTrackers.Add(new OperatorTracker(lastOperatorIndex, j));
                            lastOperatorIndex = j + 1;
                            j = query.Length;
                        }
                    }
                }
                else if (CalculationType.isCharacterAnOperator(currentCharacter))
                {
                    lastOperatorIndex = characterPositionInString + 1;
                }

                if (isEndOfExpression(characterPositionInString))
                {
                    operatorTrackers.Add(new OperatorTracker(lastOperatorIndex, characterPositionInString + 1));
                }

                if (isOperatorsResolvable(operatorTrackers))
                {
                    this.resolveCalculation(operatorTrackers, type);

                    lastOperatorIndex = 0;
                    characterPositionInString = 0;
                    operatorTrackers.Clear();
                }
            }

            return this;
        }

        private bool isOperatorsResolvable(List<OperatorTracker> operatorTrackers)
        {
            if (operatorTrackers.Count == 2)
            {
                return true;
            }

            return false;
        }

        private bool isEndOfExpression(int i)
        {
            return i == query.Length - 1;
        }

        private bool isCharacterTheCurrentOperatorType(char currentCharacter, CalculationType calculationType)
        {
            return currentCharacter == calculationType.getOperatorType();
        }

        private CalculationType samePriorityCalculations(char currentCharacter, CalculationType currentCalculationType)
        {
            if(!CalculationType.isCharacterAnOperator(currentCharacter)) return currentCalculationType;
            
            Dictionary<char, CalculationType> samePriorityCalculations = CalculationType.getOrderOf()[currentCalculationType.getPriority()];

            try
            {
                currentCalculationType = samePriorityCalculations[currentCharacter];
                return samePriorityCalculations[currentCharacter];
            }
            catch (Exception e)
            {
                return currentCalculationType;
            }
        }

        private void resolveCalculation(List<OperatorTracker> trackers, CalculationType currentCalculationType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(query);

            decimal number1 = Convert.ToDecimal(query.Substring(trackers[0].getIndexStart(),
                trackers[0].getIndexEnd() - trackers[0].getIndexStart()));
            decimal number2 = Convert.ToDecimal(query.Substring(trackers[1].getIndexStart(),
                trackers[1].getIndexEnd() - trackers[1].getIndexStart()));

            String tmp = Convert.ToString(currentCalculationType.calculate(number1, number2));

            stringBuilder.Remove(trackers[0].getIndexStart(), trackers[1].getIndexEnd() - trackers[0].getIndexStart())
                .Insert(trackers[0].getIndexStart(), tmp);
            query = stringBuilder.ToString();
            System.Console.WriteLine(query);
        }

        public String getResult()
        {
            return this.query;
        }
    }
}