using ExchangeRatesApi.Models;

namespace ExchangeRatesApi.Interfaces;

public interface IBankExchangeRatesAPI
{
    Task<SingleCurrency> GetCurrencyByDate(string currencyCode, string date);
    Task<SingleCurrency> GetLastNQuotations(string table,string currencyCode, int n);
}
