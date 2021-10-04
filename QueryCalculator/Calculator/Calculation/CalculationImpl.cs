using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryCalculator.Calculator.Calculations
{
    class CalculationImpl : Calculation
    {
        private String query;
        private CalculationType currentCalculationType;

        public CalculationImpl(String query)
        {
            this.query = query;
        }

        public List<OperatorTracker> findExpression(CalculationType type)
        {
            currentCalculationType = type;
            List<OperatorTracker> trackers = new();

            for (int i = 0; i < query.Length; i++)
            {
                char ch = query[i];

                if (isCharacterTheCurrentOperatorType(ch))
                {
                    if (trackers.Count == 0)
                    {
                        trackers.Add(new OperatorTracker(0, i));
                    }
                    else
                    {
                        trackers.Add(new OperatorTracker(trackers[0].getIndexEnd(), i));
                    }
                }

                if (isAnOperatorButNotTheCurrentOne(type, ch))
                {
                    trackers.Add(new OperatorTracker(trackers[0].getIndexEnd() + 1, i));
                }

                if (isEndOfExpression(i))
                {
                    trackers.Add(new OperatorTracker(trackers[1].getIndexEnd(), i));
                }

                if (trackers.Count == 2)
                {
                    return trackers;
                }
            }

            return null;
        }

        private bool isAnOperatorButNotTheCurrentOne(CalculationType type, char ch)
        {
            return CalculationType.isCharacterAnOperator(ch) && ch != type.getOperatorType();
        }

        public Calculation run(CalculationType type)
        {
            this.currentCalculationType = type;

            int lastOperatorIndex = 0;
            List<OperatorTracker> operatorTrackers = new();

            for (int characterPositionInString = 0;
                characterPositionInString < query.Length;
                characterPositionInString++)
            {
                char currentCharacter = query[characterPositionInString];

                type = samePriorityCalculations(currentCharacter);

                if (isCharacterTheCurrentOperatorType(currentCharacter))
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
                    this.resolveCalculation(operatorTrackers);

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

        private bool isCharacterTheCurrentOperatorType(char currentCharacter)
        {
            return currentCharacter == currentCalculationType.getOperatorType();
        }

        private CalculationType samePriorityCalculations(char currentCharacter)
        {
            if(!CalculationType.isCharacterAnOperator(currentCharacter)) return currentCalculationType;
            
            CalculationType calculationType = CalculationType.getTypeDynamically(currentCharacter);

            Dictionary<char, CalculationType> samePriorityCalculations = CalculationType.getOrderOf()[currentCalculationType.getOrder()];

            try
            {
                this.currentCalculationType = samePriorityCalculations[currentCharacter]);
                return samePriorityCalculations[currentCharacter];
            }
            catch (Exception e)
            {
                
            }



            samePriorityCalculation(currentCharacter, CalculationType.getTypeDynamically('/'),
                CalculationType.getTypeDynamically('*'));
            samePriorityCalculation(currentCharacter, CalculationType.getTypeDynamically('+'),
                CalculationType.getTypeDynamically('-'));

            return currentCalculationType;
        }

        private void samePriorityCalculation(char currentCharacter, CalculationType firstType,
            CalculationType secondType)
        {
            if (currentCalculationType == firstType || currentCalculationType == secondType)
            {
                if (currentCharacter == firstType.getOperatorType() || currentCharacter == secondType.getOperatorType())
                {
                    currentCalculationType = CalculationType.getTypeDynamically(currentCharacter);
                }
            }
        }

        private void resolveCalculation(List<OperatorTracker> trackers)
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