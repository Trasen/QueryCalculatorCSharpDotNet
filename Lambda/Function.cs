using System;
using Amazon.Lambda.Core;
using QueryCalculator.Calculator;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Lambda
{
    public class Function
    {

        static Function()
        {
             new DataStoreFactory(new DataStore());
        }

        public string FunctionHandler(string input, ILambdaContext context)
        {
            Calculator calculator = new CalculatorImpl();
            return calculator.calculate(input);
        }
    }
}