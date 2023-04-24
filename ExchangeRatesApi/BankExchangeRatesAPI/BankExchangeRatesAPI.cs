using ExchangeRatesApi.Interfaces;
using ExchangeRatesApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace ExchangeRatesApi.BankExchangeRatesApi;


// Get data from NBP web api
public class BankExchangeRatesAPI : IBankExchangeRatesAPI
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<BankExchangeRatesAPI> _logger;
    private string _baseUrl;

    public BankExchangeRatesAPI(HttpClient httpClient, 
        IConfiguration configuration, 
        ILogger<BankExchangeRatesAPI> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        _baseUrl = _configuration.GetValue<string>("NBPWebApi:BaseUrl");

        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    }
    public async Task<SingleCurrency> GetCurrencyByDate(string currencyCode, string date)
    {

        var response = await _httpClient.GetAsync($"/api/exchangerates/rates/a/{currencyCode}/{date}");

        return await CheckResponse(response);

    }

    public async Task<SingleCurrency> GetLastNQuotations(string table,string currencyCode, int n)
    {
        var response = await _httpClient.GetAsync($"/api/exchangerates/rates/{table}/{currencyCode}/last/{n}");

        return await CheckResponse(response);
    }

    private async Task<SingleCurrency> CheckResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(response.RequestMessage.ToString());
            return null;
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SingleCurrency>(responseBody);
        }
    }
}
