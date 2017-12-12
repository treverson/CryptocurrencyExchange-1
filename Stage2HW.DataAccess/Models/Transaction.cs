﻿using System;

namespace Stage2HW.DataAccess.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
    }
}
