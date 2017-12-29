using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Stage2HW.DataAccess.Repositories
{
    public class BaseAzureTableStorageRepository
    {
        public CloudTable TableReference;

        public BaseAzureTableStorageRepository(string tableName)
        {
            var cloudStorageAccount =
                CloudStorageAccount.Parse(
                    ConfigurationManager.AppSettings["exchangeAzureTableStorageConnectionString"]);

            var tableClient = cloudStorageAccount.CreateCloudTableClient();

            TableReference = tableClient.GetTableReference(tableName);

            TableReference.CreateIfNotExists();
        }
    }
}
