# ExchangeRatesApi
## Description
REST Api project, which is getting a data from Narodowy Bank Polski's public APIs (http://api.nbp.pl/) and return relevant information from them,
## Operations
1. Given a date (formatted YYYY-MM-DD) and a currency code, provide its average exchange rate.
2. Given a currency code and the number of last quotations N (N <= 255), provide the max and min average value.
3. Given a currency code and the number of last quotations N (N <= 255), provide the major difference between the buy and ask rate.
## Examples of use
### To start the app, run this command in project folder:
* dotnet run
### To query operation 1, run this command (which should have the value 5.2768 as the returning information):
* curl https://localhost:7298/api/ExchangeRates/gbp/2023-01-02
### To query operation 2, run this command (which should have the minAverage 5.2086 and maxAverage 5.4638 as the returning information):
* curl https://localhost:7298/api/ExchangeRates/gbp/last/100
### To query operation 3, run this command (which should have the date 2022-12-14 and difference 0.1096 as the returning information):
* curl https://localhost:7298/api/ExchangeRates/gbp/difference/last/100
