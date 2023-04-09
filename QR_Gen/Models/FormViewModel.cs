using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace QR_Gen.ViewModels
{
    public class FormViewModel
    {
        [Key]
        public int id { get; set; }
        public string type { get; set; }
        public string timestamp { get; set; }
        public string QRCode { get; set; }
        public int SelectedColor { get; set; }
        public string URLData { get; set; }
        public string phoneNumber { get; set; }
        public string SMSMessage { get; set; }
        public string emailAddress { get; set; }
        public string emailSubject { get; set; }
        public string emailBody { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string workNumber { get; set; }
        public string organization { get; set; }
        public string houseNumber { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string note { get; set; }
    }

    public class QRFormsDBContext : DbContext
    {
        public QRFormsDBContext() { }
        public DbSet<FormViewModel> Forms { get; set; }
    }
}