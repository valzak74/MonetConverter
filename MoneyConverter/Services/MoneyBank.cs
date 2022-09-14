using MoneyConverter.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Reflection;

namespace MoneyConverter.Services
{
    public class MoneyBank : IMoneyBank
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MoneyBank> _logger;
        public MoneyBank(ILogger<MoneyBank> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<Rate> GetRates(CancellationToken cancellationToken)
        {
            try
            {
                string request = "https://www.cbr-xml-daily.ru/latest.js";
                using var httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response =
                    (await httpClient.GetAsync(request, cancellationToken)).EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseCbr = JsonConvert.DeserializeObject<ResponseCbr>(responseBody,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                if ((responseCbr != null) && (responseCbr.Rates != null))
                    return responseCbr.Rates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            throw new Exception("Wrong Http response");
        }
        public decimal ConvertedAmount(Rate rates, decimal in_amount, Currency in_currency, Currency out_currency)
        {
            decimal result = 0;
            if (in_amount != 0)
            {
                if (in_currency != out_currency)
                {
                    var rate = rates.GetType().GetProperty(in_currency.ToString());
                    var in_RUB = in_amount / (decimal)(rate?.GetValue(rates) ?? 1m);

                    rate = rates.GetType().GetProperty(out_currency.ToString());
                    result += (decimal)(rate?.GetValue(rates) ?? 1m) * in_RUB;
                }
                else
                    result += in_amount;
            }

            return decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}
