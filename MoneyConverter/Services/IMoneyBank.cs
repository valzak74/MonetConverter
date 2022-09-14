using MoneyConverter.Models;

namespace MoneyConverter.Services
{
    public interface IMoneyBank
    {
        Task<Rate> GetRates(CancellationToken cancellationToken);
        decimal ConvertedAmount(Rate rates, decimal in_amount, Currency in_currency, Currency out_currency);
    }
}
