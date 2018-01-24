using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;
using Stage2HW.Business.Services;
using Stage2HW.Business.Services.Interfaces;
using Stage2HW.Cli.Services;
using Stage2HW.DataAccess.Data;
using Stage2HW.WebApi.AppStart;
using Stage2HW.WebApi.Configuration;
using Stage2HW.WebApi.Controllers;
using Stage2HW.Cli.Services.Interfaces;

namespace Stage2HW.WebApi
{
    internal class Program
    {
        public static readonly ICurrencyExchangeConfig CurrencyExchangeConfig = new AppConfig();

        static void Main()
        {
          //  string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<StartUp>(CurrencyExchangeConfig.LocalHostAddress))
            {
                
                var httpClient = new HttpClient();

                var response = httpClient.GetAsync(CurrencyExchangeConfig.LocalHostAddress + "Exchange/status").Result;

                Console.WriteLine($"{response}\n" +
                                  $"{response.Content.ReadAsStringAsync().Result}\n" +
                                  $"Status code: {response.StatusCode}");
                
                Console.ReadLine();

            }
        }
    }
}
