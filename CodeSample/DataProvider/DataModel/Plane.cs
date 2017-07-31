using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider.DataModel
{
    public class Plane : TableEntity
    {
        public Plane(string manufacturer, string registration)
        {
            this.PartitionKey = manufacturer;
            this.RowKey = registration;
        }

        public Plane() { }

        [Microsoft.WindowsAzure.Storage.Table.IgnoreProperty]
        public string Manufacturer
        {
            get
            {
                return this.PartitionKey;
            }
        }
        [Microsoft.WindowsAzure.Storage.Table.IgnoreProperty]
        public string Registration
        {
            get
            {
                return this.RowKey;
            }
        }
        [Microsoft.WindowsAzure.Storage.Table.IgnoreProperty]
        public DateTime Created
        {
            get
            {
                return this.Timestamp.Date;
            }
        }
        public string Type { get; set; }
        public string ModeSCode { get; set; }
    }
}
