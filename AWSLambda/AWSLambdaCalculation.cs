using System;
using Amazon.Lambda.Core;

namespace AWSLambda
{
    public class AWSLambdaCalculation
    {
        public string Calculate(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }
    }
}