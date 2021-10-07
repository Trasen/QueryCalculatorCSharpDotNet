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
            List<IndexTracker> operatorTrackers = new();

            for (int characterPositionInString = 0;
                characterPositionInString < query.Length;
                characterPositionInString++)
            {
                char currentCharacter = query[characterPositionInString];

                type = type.samePriorityCalculations(currentCharacter);

                if (type.isCharacterTheCurrentOperatorType(currentCharacter))
                {
                    
                    
                    if(type.getOperatorType() != '-') {
                    operatorTrackers.Add(new IndexTracker(lastOperatorIndex, characterPositionInString));
                    }
                    else
                    {
                        if (characterPositionInString != 0)
                        {
                            if (Char.IsNumber(query[characterPositionInString -1]))
                            {
                                operatorTrackers.Add(new IndexTracker(lastOperatorIndex, characterPositionInString));
                            } else if(!Char.IsNumber(query[characterPositionInString -1]))
                            {
                                
                            }
                        }
                        
                        else
                        {
                            continue;
                        }
                    }

                    lastOperatorIndex = characterPositionInString + 1;

                    for (int j = characterPositionInString + 1; j < query.Length; j++)
                    {
                        char nestedCharacter = query[j];

                        if (CalculationType.isCharacterAnOperator(nestedCharacter))
                        {
                            operatorTrackers.Add(new IndexTracker(lastOperatorIndex, j));
                            lastOperatorIndex = j + 1;
                            j = query.Length;
                        }
                    }
                }
                else if (CalculationType.isCharacterAnOperator(currentCharacter))
                {
                    if (currentCharacter != '-')
                    {
                        lastOperatorIndex = characterPositionInString + 1;
                    }

                }

                if (characterPositionInString == query.Length - 1)
                {
                    operatorTrackers.Add(new IndexTracker(lastOperatorIndex, characterPositionInString + 1));
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

        private bool isOperatorsResolvable(List<IndexTracker> operatorTrackers)
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