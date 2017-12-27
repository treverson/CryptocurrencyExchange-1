﻿using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Stage2HW.DataAccess.Models
{
    public class Transaction : TableEntity
    {
        public int Id { get; set; }
       // public int UserId { get; set; }
        public int UserId
        {
            get => Int32.Parse(PartitionKey);
            set => PartitionKey = value.ToString();
        }
        //public string CurrencyName { get; set; }
        public string CurrencyName
        {
            get => RowKey;
            set => RowKey = value;
        }
        public DateTime TransactionDate { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public double Fiat { get; set; }
    }
}
