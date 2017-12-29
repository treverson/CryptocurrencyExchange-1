using Microsoft.WindowsAzure.Storage.Table;

namespace Stage2HW.DataAccess.Models
{
    public class Transaction : TableEntity
    {
        [IgnoreProperty]
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public double ExchangeRate { get; set; }
        public double Amount { get; set; }
        public double Fiat { get; set; }

        public int UserId
        {
            get => int.Parse(PartitionKey);
            set => PartitionKey = value.ToString();
        }
        public string TransactionDate
        {
            get => RowKey;
            set => RowKey = value;
        }
    }
}
