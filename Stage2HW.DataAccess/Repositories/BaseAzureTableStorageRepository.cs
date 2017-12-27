using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace Stage2HW.DataAccess.Repositories
{
    public class BaseAzureTableStorageRepository
    {
        public CloudTable TableReference;

        public BaseAzureTableStorageRepository(string tableName)
        {
            var cloudStorageAccount = 
                CloudStorageAccount.Parse(ConfigurationManager.AppSettings["azureExchangeTableStorageConnectionString"]);

            var tableClient = cloudStorageAccount.CreateCloudTableClient();

            TableReference = tableClient.GetTableReference(tableName);

            TableReference.CreateIfNotExists();
            //if (!TableReference.CreateIfNotExists())
            //{
            //    Console.WriteLine("Table {0} already exists", TableReference.Name);
            //    return;
            //}

            //Console.WriteLine("Table {0} connected", TableReference.Name);
        }
    }
}
