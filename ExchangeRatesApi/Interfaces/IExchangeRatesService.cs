using ExchangeRatesApi.Models;

namespace ExchangeRatesApi.Interfaces;

public interface IExchangeRatesService
{
    public Task<decimal> GetCurrencyAverage(string currencyCode, string date);
    public Task<MinMaxAverage> GetMinMaxAverage(string currencyCode, int n);
    public Task<AskBidDifference> GetMajorDifference(string currencyCode, int n);
}
