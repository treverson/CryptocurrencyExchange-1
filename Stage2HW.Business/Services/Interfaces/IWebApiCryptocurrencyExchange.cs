using Stage2HW.Business.Services;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IWebApiCryptocurrencyExchange
    {
        void RunExchange();
        void UpdateValues(NewExchangeRatesEventArgs e);
    }
}