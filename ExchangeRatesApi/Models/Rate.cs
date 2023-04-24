namespace ExchangeRatesApi.Models
{
    public class Rate
    {
        public string No { get; set; }
        public DateTime? TradingDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Ask { get; set; }
        public decimal? Mid { get; set; }

    }
}