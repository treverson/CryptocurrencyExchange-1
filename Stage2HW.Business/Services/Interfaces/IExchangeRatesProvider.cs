using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public interface IExchangeRatesProvider
    {
        void RunProvider();
        event RatesDownloadedHandler NewRatesDownloadedEvent;
        List<Currency> Currencies { get; set; }
    }
}