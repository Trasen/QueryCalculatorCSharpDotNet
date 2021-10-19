using System;
using Amazon.Lambda.Core;
using QueryCalculator.Calculator;

namespace AWSLambda
{
    public class AWSLambdaCalculation
    {
        
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public string Calculate(string input, ILambdaContext context)
        {

            Calculator calculator = new CalculatorImpl();
            return calculator.calculate(input);
        }
    }
}