using System;

namespace Stage2HW.Business.Services
{
    public class RatesGeneratedEventArgs : EventArgs
    {
        public double BitCoinValue;
        public double BitCoinCashValue;
        public double EthereumValue;
        public double LiteCoinValue;
    }
}
