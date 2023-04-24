namespace ExchangeRatesApi.Models;

public class SingleCurrency
{
    public string Table { get; set; }
    public string Currency { get; set; }
    public string Code { get; set; }
    public List<Rate> Rates { get; set; }

}
