using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider.Common
{
    /// <summary>
    /// Helper for use Azure services
    /// </summary>
    public class AzureHelper
    {
        /// <summary>
        /// Create all tables in storage 
        /// </summary>
        /// <param name="connectionString">String with data for connect to Azure Storage account</param>
        public static void InitializeDataStructure(string connectionString)
        {
            AddTable(connectionString, Common.Constants.Entities.Plane.Name);
            AddTable(connectionString, Common.Constants.Entities.FlightInfo.Name);
        }

        /// <summary>
        /// Create table in azure storage
        /// </summary>
        /// <param name="connectionString">String with data for connect to Azure Storage account</param>
        /// <param name="tableName">Name of new table</param>
        internal static void AddTable(string connectionString, string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            table.CreateIfNotExistsAsync();
        }

        /// <summary>
        /// Add row to table
        /// </summary>
        /// <param name="connectionString">String with data for connect to Azure Storage account</param>
        /// <param name="tableName">Name of table where will be created row</param>
        /// <param name="newObject">Data of object</param>
        public static void AddEntity(string connectionString, string tableName, TableEntity newObject)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            TableOperation insertOperation = TableOperation.Insert(newObject);
            
            table.ExecuteAsync(insertOperation);
        }

        /// <summary>
        /// Get table adapter for get data from Azure Storage
        /// </summary>
        /// <param name="connectionString">String with data for connect to Azure Storage account</param>
        /// <param name="tableName">Name of table where storage data</param>
        /// <returns></returns>
        public static CloudTable GetTable(string connectionString, string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }
    }
}
