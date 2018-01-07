using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Stage2HW.WebApi
{
    internal class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<StartUp>(baseAddress))
            {
                var httpClient = new HttpClient();

                var response = httpClient.GetAsync(baseAddress + "Exchange/status").Result;

                Console.WriteLine($"{response}\n" +
                                  $"{response.Content.ReadAsStringAsync().Result}\n" +
                                  $"Status code: {response.StatusCode}");
                
                Console.ReadLine();
            }
        }
    }
}
