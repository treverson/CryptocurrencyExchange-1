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
                CloudStorageAccount.Parse(
                    ConfigurationManager.AppSettings["exchangeAzureTableStorageConnectionString"]);

            var tableClient = cloudStorageAccount.CreateCloudTableClient();

            TableReference = tableClient.GetTableReference(tableName);

            TableReference.CreateIfNotExists();
        }
    }
}
