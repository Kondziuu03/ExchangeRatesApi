using ExchangeRatesApi.Interfaces;
using ExchangeRatesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesApi.Controllers;

[Route("api/[controller]/{currencyCode}")]
[ApiController]
public class ExchangeRatesController : ControllerBase
{
    private readonly IExchangeRatesService _exchangeRatesService;

    public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
    {
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet("{date}")]
    public async Task<ActionResult<decimal>> GetCurrencyAverage([FromRoute]string currencyCode, [FromRoute]string date)
    {
        var result = await _exchangeRatesService.GetCurrencyAverage(currencyCode, date);
        return Ok(result);
    }

    [HttpGet("last/{n}")]
    public async Task<ActionResult<MinMaxAverage>> GetMinMaxAverage([FromRoute]string currencyCode, [FromRoute]int n)
    {
        var result = await _exchangeRatesService.GetMinMaxAverage(currencyCode, n);
        return Ok(result);
    }

    [HttpGet("difference/last/{n}")]
    public async Task<ActionResult<AskBidDifference>> GetMajorDifference([FromRoute] string currencyCode, [FromRoute] int n)
    {
        var result = await _exchangeRatesService.GetMajorDifference(currencyCode, n);
        return Ok(result);
    }
}
