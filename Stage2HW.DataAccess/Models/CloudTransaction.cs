using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Stage2HW.DataAccess.Models
{
    internal class CloudTransaction : TableEntity
    {
        public int UserId
        {
            get => int.Parse(PartitionKey);
            set => PartitionKey = value.ToString();
        }
        public Guid TransactionGuid
        {
            get => new Guid(RowKey);
            set => RowKey = value.ToString("D");
        }
        public DateTime TransactionDate { get; set; }
        public string CurrencyName { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public double Fiat { get; set; }
    }
}
