using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataProvider.DataModel;
using Microsoft.WindowsAzure.Storage.Table;
using DataProvider.Common;

namespace PlaneManagmentSystem.Controllers
{
    public class PlanesController : Controller
    {
        // Connection string to data storages
        private string connString = "DefaultEndpointsProtocol=https;AccountName=codesample;AccountKey=qmAJQAeBuHL3QSnyUmSJglGm7ExEInszHAMM/+aRJRiXQTMOkoS4tI8tYo+cc5mXuiw5hRti46WXyb0g3me6JA==;EndpointSuffix=core.windows.net";

        // GET: Planes
        public ActionResult Index()
        {
            PlaneManagmentSystem.Services.Plane planeClient = new Services.Plane();
            var planesData = planeClient.Get();

            return View(planesData);
        }

        // GET: Planes/Details/5
        public ActionResult Details(string id)
        {
            PlaneManagmentSystem.Services.Plane planeClient = new Services.Plane();
            var planeObj = planeClient.Get(id);
            
            return View(planeObj);
        }

        // GET: Planes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // Prepare object for send to Azure storage
                DataProvider.DataModel.Plane planeObj = new DataProvider.DataModel.Plane(collection["Manufacturer"], collection["Registration"]);
                planeObj.Type = collection["Type"];
                planeObj.ModeSCode = collection["ModeSCode"];
                
                // Create row in table
                DataProvider.Common.AzureHelper.AddEntity(connString, DataProvider.Common.Constants.Entities.Plane.Name, planeObj);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Planes/Edit/5
        public ActionResult Edit(string id)
        {
            PlaneManagmentSystem.Services.Plane planeClient = new Services.Plane();
            var planeObj = planeClient.Get(id);
            
            return View(planeObj);
        }

        // POST: Planes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                // Create a retrieve operation that takes a plane entity.
                TableOperation retrieveOperation = TableOperation.Retrieve<DataProvider.DataModel.Plane>(
                    collection[DataProvider.Common.Constants.Entities.Plane.Columns.Manufacturer], 
                    collection[DataProvider.Common.Constants.Entities.Plane.Columns.Registration]);

                var table = AzureHelper.GetTable(connString, Constants.Entities.Plane.Name);

                // Execute the operation.
                var retrievedResult = table.ExecuteAsync(retrieveOperation).Result;
                var updateEntity = (DataProvider.DataModel.Plane)retrievedResult.Result;

                if (updateEntity != null)
                {
                    bool userChangeProperty = false;

                    // Check can user change any of properties
                    if (updateEntity.ModeSCode != collection[Constants.Entities.Plane.Columns.ModeSCode])
                    {
                        updateEntity.ModeSCode = collection[Constants.Entities.Plane.Columns.ModeSCode];
                        userChangeProperty = true;
                    }

                    if (updateEntity.Type != collection[Constants.Entities.Plane.Columns.Type])
                    {
                        updateEntity.Type = collection[Constants.Entities.Plane.Columns.Type];
                        userChangeProperty = true;
                    }

                    // If user change some property(s), send data to storage
                    if (userChangeProperty)
                    {
                        TableOperation updateOperation = TableOperation.Replace(updateEntity);
                        
                        table.ExecuteAsync(updateOperation);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(string id)
        {
            PlaneManagmentSystem.Services.Plane planeClient = new Services.Plane();
            var planeObj = planeClient.Get(id);
            
            return View(planeObj);
        }

        // POST: Planes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // Get item
                PlaneManagmentSystem.Services.Plane planeClient = new Services.Plane();
                var planeObj = planeClient.Get(id);

                // Prepare deleto operation
                TableOperation retrieveOperation = TableOperation.Retrieve<DataProvider.DataModel.Plane>(planeObj.Manufacturer, planeObj.Registration);

                var table = AzureHelper.GetTable(connString, Constants.Entities.Plane.Name);
                var retrievedResult = table.ExecuteAsync(retrieveOperation).Result;

                var deleteEntity = (DataProvider.DataModel.Plane)retrievedResult.Result;

                if (deleteEntity != null)
                {
                    // execute opertion
                    TableOperation updateOperation = TableOperation.Delete(deleteEntity);

                    table.ExecuteAsync(updateOperation);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}