using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlaneManagmentSystem.Services
{
    [Route("api/[controller]")]
    public class Plane : Controller
    {
        // Connection string to data storages
        private string connString = "DefaultEndpointsProtocol=https;AccountName=codesample;AccountKey=qmAJQAeBuHL3QSnyUmSJglGm7ExEInszHAMM/+aRJRiXQTMOkoS4tI8tYo+cc5mXuiw5hRti46WXyb0g3me6JA==;EndpointSuffix=core.windows.net";

        // GET api/values/5
        [HttpGet("{regID}")]
        public string Get(string regID)
        {
            // Get data from Azure storage and display in page
            var table = DataProvider.Common.AzureHelper.GetTableReader(connString,
                    DataProvider.Common.Constants.Entities.Plane.Name);
            var query = new TableQuery<DataProvider.DataModel.Plane>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, regID));

            var result = table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());
            var planeInfoResult = result.Result;

            if (planeInfoResult.Count() > 0)
            {
                var planeInfo = planeInfoResult.First();
                return Newtonsoft.Json.JsonConvert.SerializeObject(planeInfo);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(new { Message = "Not found" });
        }
    }
}
