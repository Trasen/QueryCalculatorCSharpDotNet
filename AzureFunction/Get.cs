
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QueryCalculator.Calculator; 

namespace AzureFunction
{
    public static class Get
    {
        [FunctionName("Get")]
        public static string RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, ILogger log)
        {
            Calculator calculator = new CalculatorImpl();
            string response = calculator.calculate(req.Query.First().Key);
            return response;
        }
    }
}