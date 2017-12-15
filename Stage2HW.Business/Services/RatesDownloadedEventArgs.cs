using Stage2HW.Business.Dtos;
using System;
using System.Collections.Generic;

namespace Stage2HW.Business.Services
{
    public class RatesDownloadedEventArgs : EventArgs
    {
        public List<Currency> CurrenciesList;
    }
}
