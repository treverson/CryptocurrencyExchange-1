using System.Collections.Generic;
using Stage2HW.Business.Dtos;

namespace Stage2HW.Business.Services.Interfaces
{
    public delegate void NewExchangeRatesHandler(NewExchangeRatesEventArgs e);

    public interface IExchangeRatesProvider
    {
        void Run();
        List<Currency> Currencies { get; set; }
        event NewExchangeRatesHandler NewExchangeRatesEvent;
    }
}