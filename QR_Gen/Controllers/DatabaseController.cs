using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QR_Gen.ViewModels;

namespace QR_Gen.Controllers
{
    public class DatabaseController : Controller
    {
        private QRFormsDBContext db = new QRFormsDBContext();

        // GET: Database
        public ActionResult Display()
        {
            return View(db.Forms.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeStamp,URLData,phoneNumber,SMSMessage," +
            "emailAddress,emailSubject,emailBody,firstName,lastName,workNumber,organization," +
            "houseNumber,street,city,state,zipCode,note")] FormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Forms.Add(formViewModel);
                db.SaveChanges();
                return RedirectToAction("Display");
            }

            return View(formViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
