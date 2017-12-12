using System;
using System.Collections.Generic;

namespace Stage2HW.Business.Services
{
    public class RatesGeneratedEventArgs : EventArgs
    {
        public List<CryptoCurrencyGenerated> CurrenciesList;
    }
}
