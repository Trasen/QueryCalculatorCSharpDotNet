using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueryCalculator.Calculator;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get(string calculation)
        {
            ICalculator calculator = new CalculatorImpl();
            return calculator.calculate(calculation);
        }
        
    }
}