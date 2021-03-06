﻿using System;
using System.Configuration;
using Stage2HW.Business.Services.Interfaces;

namespace Stage2HW.Cli.Configuration
{
    internal class AppConfig : ICurrencyExchangeConfig
    {
        public int RefreshTime => Convert.ToInt32(ConfigurationManager.AppSettings["refreshTime"]);
        public string BitBayBtcPlnAddress => ConfigurationManager.AppSettings["bitBayBTCPLNvalue"];
        public string BitBayBccPlnAddress => ConfigurationManager.AppSettings["bitBayBCCPLNvalue"];
        public string BitBayEthPlnAddress => ConfigurationManager.AppSettings["bitBayETHPLNvalue"];
        public string BitBayLtcPlnAddress => ConfigurationManager.AppSettings["bitBayLTCPLNvalue"];
        public string LocalHostAddress => ConfigurationManager.AppSettings["localHostAddress"];
        public string DataBaseType => ConfigurationManager.AppSettings["dataBaseType"];
        public string ExchangeType => ConfigurationManager.AppSettings["exchangeType"];
    }
}
