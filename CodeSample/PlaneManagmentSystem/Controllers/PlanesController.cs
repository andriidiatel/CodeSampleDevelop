using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataProvider.DataModel;
using Microsoft.WindowsAzure.Storage.Table;

namespace PlaneManagmentSystem.Controllers
{
    public class PlanesController : Controller
    {
        // Connection string to data storages
        private string connString = "DefaultEndpointsProtocol=https;AccountName=codesample;AccountKey=qmAJQAeBuHL3QSnyUmSJglGm7ExEInszHAMM/+aRJRiXQTMOkoS4tI8tYo+cc5mXuiw5hRti46WXyb0g3me6JA==;EndpointSuffix=core.windows.net";

        // GET: Planes
        public ActionResult Index()
        {
            // Get data from Azure storage and display in page
            var table = DataProvider.Common.AzureHelper.GetTableReader(connString,
                    DataProvider.Common.Constants.Entities.Plane.Name);
            var query = new TableQuery<Plane>();

            var result = table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            return View(result.Result);
        }

        // GET: Planes/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Planes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Planes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}