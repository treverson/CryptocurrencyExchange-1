﻿using System;

namespace Stage2HW.DataAccess.Models
{
    public class Transaction
    {
        public int UserId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CurrencyName { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public double Fiat { get; set; }
    }
}
