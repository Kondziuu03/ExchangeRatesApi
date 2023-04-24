using ExchangeRatesApi.Exceptions;
using ExchangeRatesApi.Interfaces;
using ExchangeRatesApi.Models;
using System.Globalization;

namespace ExchangeRatesApi.Services;

public class ExchangeRatesService : IExchangeRatesService
{
    private readonly IBankExchangeRatesAPI _bankExchangeRatesAPI;
    private readonly string tableA = "a";
    private readonly string tableC = "c";

    public ExchangeRatesService(IBankExchangeRatesAPI bankExchangeRatesAPI)
    {
        _bankExchangeRatesAPI = bankExchangeRatesAPI;
    }

    //return average exchange rate 
    public async Task<decimal> GetCurrencyAverage(string currencyCode, string date)
    {

        if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
            throw new BadRequestException("Wrong date format.");

        SingleCurrency singleCurrency = await _bankExchangeRatesAPI.GetCurrencyByDate(currencyCode, date);

        if (singleCurrency == null)
            throw new NotFoundException("Not found");

        Rate rate = singleCurrency.Rates.FirstOrDefault();
        if (rate == null)
            throw new NotFoundException("Not Found");
        else
            return Convert.ToDecimal(rate.Mid);
        
    }

    //return max and min average value in last n quotations
    public async Task<MinMaxAverage> GetMinMaxAverage(string currencyCode, int n)
    {
        var ratesList = await GetRatesList(tableA, currencyCode, n);

        decimal minAverage = Convert.ToDecimal(ratesList.Min(x => x.Mid));
        decimal maxAverage = Convert.ToDecimal(ratesList.Max(x => x.Mid));

        return new MinMaxAverage { MinAverage = minAverage, MaxAverage = maxAverage};
    }

    //return major difference between buy and ask rate in last n quotations
    public async Task<AskBidDifference> GetMajorDifference(string currencyCode, int n)
    {
        var ratesList = await GetRatesList(tableC,currencyCode,n);
        var rate = ratesList.OrderByDescending(x => Math.Abs(Convert.ToDecimal(x.Ask) - Convert.ToDecimal(x.Bid))).First();

        return new AskBidDifference { 
            Date = rate.EffectiveDate.ToShortDateString(),
            Difference = Math.Abs(Convert.ToDecimal(rate.Ask) - Convert.ToDecimal(rate.Bid))
        };
    }


    private async Task<List<Rate>> GetRatesList(string table, string currencyCode, int n)
    {
        if (n > 255 || n < 0)
            throw new BadRequestException("Wrong N number");

        SingleCurrency singleCurrency = await _bankExchangeRatesAPI.GetLastNQuotations(table, currencyCode, n);
        if (singleCurrency == null)
            throw new NotFoundException("Not found");
        if (!singleCurrency.Rates.Any())
            throw new NotFoundException("NotFound");

        return singleCurrency.Rates;
    }
}
