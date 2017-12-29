using System;

namespace Stage2HW.Business.Dtos
{
    public class TransactionDto
    {
       // public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyName { get; set; }

        public DateTime TransactionDate { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public double Fiat { get; set; }
    }
}
