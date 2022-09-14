using Newtonsoft.Json;

namespace MoneyConverter.Models
{
    [JsonConverter(typeof(DefaultUnknownEnumConverter), (int)NotFound)]
    public enum Currency
    {
        RUB,
        USD,
        EUR,
        AUD,
        AZN,
        GBP,
        AMD,
        BYN,
        BGN,
        BRL,
        HUF,
        HKD,
        DKK,
        INR,
        KZT,
        CAD,
        KGS,
        CNY,
        MDL,
        NOK,
        PLN,
        RON,
        XDR,
        SGD,
        TJS,
        TRY,
        TMT,
        UZS,
        UAH,
        CZK,
        SEK,
        CHF,
        ZAR,
        KRW,
        JPY,
        NotFound
    }
    public class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Money()
        {
            Amount = 0;
            Currency = Currency.RUB;
        }
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money(Currency currency) => Currency = currency;
    }
    public class RequestCalculator
    {
        public Money? Income { get; set; }
        public Currency OutCurrency { get; set; } = Currency.RUB;
        public List<Money>? AddMoney { get; set; }
    }
    public class ResponseCalculator
    {
        public Money Outcome { get; set; }
        public ResponseCalculator(Currency currency) => Outcome = new Money(currency);
    }
    
    public class ResponseCbr
    {
        public Rate? Rates { get; set; }
    }
    public class Rate
    {
        public decimal AUD { get; set; }
        public decimal AZN { get; set; }
        public decimal GBP { get; set; }
        public decimal AMD { get; set; }
        public decimal BYN { get; set; }
        public decimal BGN { get; set; }
        public decimal BRL { get; set; }
        public decimal HUF { get; set; }
        public decimal HKD { get; set; }
        public decimal DKK { get; set; }
        public decimal USD { get; set; }
        public decimal EUR { get; set; }
        public decimal INR { get; set; }
        public decimal KZT { get; set; }
        public decimal CAD { get; set; }
        public decimal KGS { get; set; }
        public decimal CNY { get; set; }
        public decimal MDL { get; set; }
        public decimal NOK { get; set; }
        public decimal PLN { get; set; }
        public decimal RON { get; set; }
        public decimal XDR { get; set; }
        public decimal SGD { get; set; }
        public decimal TJS { get; set; }
        public decimal TRY { get; set; }
        public decimal TMT { get; set; }
        public decimal UZS { get; set; }
        public decimal UAH { get; set; }
        public decimal CZK { get; set; }
        public decimal SEK { get; set; }
        public decimal CHF { get; set; }
        public decimal ZAR { get; set; }
        public decimal KRW { get; set; }
        public decimal JPY { get; set; }
    }
}
