using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueryCalculator.Calculator;

namespace AzureFunction
{
    public static class Get
    {
        [FunctionName("Get")]
        public static async Task<string> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] string calculation, ILogger log)
        {
            Calculator calculator = new CalculatorImpl();
            return calculator.calculate(calculation);
            
        }
    }
}