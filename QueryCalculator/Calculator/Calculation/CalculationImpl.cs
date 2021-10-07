using System;
using System.Collections.Generic;

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
            List<Index> operatorTrackers = new();

            for (int characterPositionInString = 0;
                characterPositionInString < query.Length;
                characterPositionInString++)
            {
                char currentCharacter = query[characterPositionInString];
                
                if(isNegativeValueAndNotOperator(characterPositionInString, currentCharacter)) continue;

                type = type.samePriorityCalculations(currentCharacter);

                if (type.isCharacterTheCurrentOperatorType(currentCharacter))
                {
                    operatorTrackers.Add(new NumberTracker(lastOperatorIndex, characterPositionInString));
                    
                    lastOperatorIndex = characterPositionInString + 1;

                    for (int j = characterPositionInString + 1; j < query.Length; j++)
                    {
                        char nestedCurrentCharacter = query[j];

                        if (isNegativeValueAndNotOperator(j, nestedCurrentCharacter)) continue;

                        if (CalculationType.isCharacterAnOperator(nestedCurrentCharacter))
                        {
                            operatorTrackers.Add(new NumberTracker(lastOperatorIndex, j));
                            lastOperatorIndex = j + 1;
                            j = query.Length;
                        }
                    }
                }
                else if (CalculationType.isCharacterAnOperator(currentCharacter))
                {
                    lastOperatorIndex = characterPositionInString + 1;
                }

                if (characterPositionInString == query.Length - 1)
                {
                    operatorTrackers.Add(new NumberTracker(lastOperatorIndex, characterPositionInString + 1));
                }

                if (isOperatorsResolvable(operatorTrackers))
                {
                    query = type.resolve(operatorTrackers, query);

                    lastOperatorIndex = 0;
                    characterPositionInString = 0;
                    operatorTrackers.Clear();
                }
            }

            return this;
        }

        private bool isNegativeValueAndNotOperator(int j, char nestedCharacter)
        {
            if (j == 0 && nestedCharacter == '-')
            {
                return true;
            }

            if (nestedCharacter == '-' && !Char.IsNumber(query[j - 1]))
            {
                return true;
            }

            return false;
        }

        private bool isOperatorsResolvable(List<Index> operatorTrackers)
        {
            if (operatorTrackers.Count == 2)
            {
                return true;
            }

            return false;
        }
        

        public String getResult()
        {
            return this.query;
        }
    }
}