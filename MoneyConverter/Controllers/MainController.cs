using Microsoft.AspNetCore.Mvc;
using MoneyConverter.Models;
using MoneyConverter.Services;
using System.Diagnostics.Eventing.Reader;

namespace MoneyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyBankController : ControllerBase
    {
        private readonly ILogger<MoneyBankController> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public MoneyBankController(ILogger<MoneyBankController> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<ResponseCalculator>> Calculate([FromBody] RequestCalculator request, CancellationToken cancellationToken)
        {
            var response = new ResponseCalculator(request.OutCurrency);
            IMoneyBank bank = _serviceScopeFactory.CreateScope()
               .ServiceProvider.GetService<IMoneyBank>();
            var currencyRates = await bank.GetRates(cancellationToken);
            if (request.OutCurrency != Currency.NotFound)
            {
                if (request.Income?.Amount > 0)
                    response.Outcome.Amount += bank.ConvertedAmount(currencyRates, request.Income.Amount, request.Income.Currency, request.OutCurrency);
                if (request.AddMoney?.Count > 0)
                    foreach (var addMoney in request.AddMoney)
                        response.Outcome.Amount += bank.ConvertedAmount(currencyRates, addMoney.Amount, addMoney.Currency, request.OutCurrency);
                return Ok(response);
            }
            else
                return NotFound("Not supported currency");

        }
    }
}