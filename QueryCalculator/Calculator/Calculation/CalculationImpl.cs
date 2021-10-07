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
            List<OperatorTracker> operatorTrackers = new();

            for (int characterPositionInString = 0;
                characterPositionInString < query.Length;
                characterPositionInString++)
            {
                char currentCharacter = query[characterPositionInString];

                
                //-2 + -2
                if (characterPositionInString == 0 && currentCharacter == '-')
                {
                    continue;
                }

                if (currentCharacter == '-' && !Char.IsNumber(query[characterPositionInString - 1]))
                {
                    continue;
                }

                type = type.samePriorityCalculations(currentCharacter);

                if (type.isCharacterTheCurrentOperatorType(currentCharacter))
                {
                    operatorTrackers.Add(new OperatorTracker(lastOperatorIndex, characterPositionInString));
                    
                    lastOperatorIndex = characterPositionInString + 1;

                    for (int j = characterPositionInString + 1; j < query.Length; j++)
                    {
                        char nestedCharacter = query[j];

                        //-2 + -2
                        if (j == 0 && nestedCharacter == '-')
                        {
                            continue;
                        }

                        if (nestedCharacter == '-' && !Char.IsNumber(query[j - 1]))
                        {
                            continue;
                        }
                        
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

                if (characterPositionInString == query.Length - 1)
                {
                    operatorTrackers.Add(new OperatorTracker(lastOperatorIndex, characterPositionInString + 1));
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

        private bool isOperatorsResolvable(List<OperatorTracker> operatorTrackers)
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