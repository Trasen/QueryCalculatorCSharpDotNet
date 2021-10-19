using System;
using Amazon.Lambda.Core;


namespace Lambda
{
    public class DataStore : QueryCalculator.Calculator.DataStore
    {
        public string Save(string value)
        {
            LambdaLogger.Log("Hello from Lambda Implementation of Save(), Value: "+ value);
            return "Hello from Lambda Implementation of Save()";
        }
    }
}