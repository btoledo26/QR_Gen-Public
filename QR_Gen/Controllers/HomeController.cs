using QR_Gen.ViewModels;
using QRCoder;
using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static QRCoder.PayloadGenerator;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace QR_Gen.Controllers
{
    public class HomeController : Controller
    {
        private QRFormsDBContext db = new QRFormsDBContext();
        private static string URLrx = "(?:https?:\\/\\/)?(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$";
        private static string PhoneRx = "^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$";
        private static string QRImageSrc = "";
        private static string QRDarkColor = "";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        private void SetColor(int selection)
        { 
            switch(selection)
            {
                case 0:
                    QRDarkColor = "#000000";
                    break;
                case 1:
                    QRDarkColor = "#d10c02";
                    break;
                case 2:
                    QRDarkColor = "#24d102";
                    break;
                case 3:
                    QRDarkColor = "#0210d1";
                    break;
                case 4:
                    QRDarkColor = "#a602d4";
                    break;
                case 5:
                    QRDarkColor = "#d46b02";
                    break;
                default:
                    break;
            }
        }

        [HttpPost]
        public ActionResult GenerateURLQR([Bind(Include = "SelectedColor,URLData")] FormViewModel form)
        {
            if(ModelState.IsValid && (form.URLData != null))
            {
                Regex rx = new Regex(URLrx);
                MatchCollection matches = rx.Matches(form.URLData);
                if (matches.Count > 0) //no matching URL found in form
                {
                    //Creates payload for QR code
                    Url generator = new Url(form.URLData);
                    SetColor(form.SelectedColor);

                    //Generates QR code with payload
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q); //generator produces payload
                    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                    QRImageSrc = qrCode.GetGraphic(20, QRDarkColor, "#ffffff"); //TODO: Set colors here

                    //Stores QRImageSrc in database w/ timestamp, type, and QR code
                    form.timestamp = DateTime.Now.ToString();
                    form.type = "URL";
                    form.QRCode = QRImageSrc;
                    db.Forms.Add(form);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpPost]
        public ActionResult GeneratePhoneQR([Bind(Include = "SelectedColor,phoneNumber")] FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                Regex rx = new Regex(PhoneRx);
                MatchCollection matches = rx.Matches(form.phoneNumber);
                if (matches.Count > 0)
                {
                    PhoneNumber generator = new PhoneNumber(form.phoneNumber);
                    SetColor(form.SelectedColor);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q);
                    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                    QRImageSrc = qrCode.GetGraphic(20, QRDarkColor, "#ffffff");

                    form.timestamp = DateTime.Now.ToString();
                    form.type = "Phone Number";
                    form.QRCode = QRImageSrc;
                    db.Forms.Add(form);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpPost]
        public ActionResult GenerateSMSQR([Bind(Include = "SelectedColor,phoneNumber,SMSMessage")] FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                Regex rx = new Regex(PhoneRx);
                MatchCollection matches = rx.Matches(form.phoneNumber);
                if (matches.Count > 0)
                {
                    SMS generator = new SMS(form.phoneNumber, form.SMSMessage);
                    SetColor(form.SelectedColor);

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q);
                    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                    QRImageSrc = qrCode.GetGraphic(20, QRDarkColor, "#ffffff");

                    form.timestamp = DateTime.Now.ToString();
                    form.type = "SMS";
                    form.QRCode = QRImageSrc;
                    db.Forms.Add(form);
                    db.SaveChanges();
                }
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpPost]
        public ActionResult GenerateEmailQR([Bind(Include = "SelectedColor,emailAddress,emailSubject,emailBody")] FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                Mail generator = new Mail(form.emailAddress, form.emailSubject, form.emailBody);
                SetColor(form.SelectedColor);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                QRImageSrc = qrCode.GetGraphic(20, QRDarkColor, "#ffffff");

                form.timestamp = DateTime.Now.ToString();
                form.type = "Email";
                form.QRCode = QRImageSrc;
                db.Forms.Add(form);
                db.SaveChanges();
            }

            ModelState.Clear();
            return View("Index");
        }

        [HttpPost]
        public ActionResult GenerateContactDataQR([Bind(Include = "SelectedColor,firstName,lastName," +
            "phoneNumber,workNumber,emailAddress, organization," +
            "houseNumber,street,city,state,zipCode,URLData,note")] FormViewModel form)
        {
            if (ModelState.IsValid)
            {
                ContactData generator = new ContactData(ContactData.ContactOutputType.VCard3, form.firstName, form.lastName,
                mobilePhone: form.phoneNumber, workPhone: form.workNumber, email: form.emailAddress, org: form.organization,
                houseNumber: form.houseNumber, street: form.street, city: form.city, stateRegion: form.state, zipCode: form.zipCode,
                website: form.URLData, note: form.note);
                SetColor(form.SelectedColor);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(generator.ToString(), QRCodeGenerator.ECCLevel.Q);
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                QRImageSrc = qrCode.GetGraphic(20, QRDarkColor, "#ffffff");

                form.timestamp = DateTime.Now.ToString();
                form.type = "Contact Data";
                form.QRCode = QRImageSrc;
                db.Forms.Add(form);
                db.SaveChanges();
            }

            ModelState.Clear();
            return View("Index");
        }

        public ActionResult DisplayQRImage()
        {
            var img = Convert.FromBase64String(QRImageSrc);
            return this.File(img, "image/png", "image.png");
        }

        [HttpPost]
        public ActionResult EmailQRCode(FormViewModel form)
        {
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);
            smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage email = new MailMessage();
            email.From = new MailAddress("QR.Gen.Project@gmail.com");
            email.To.Add(form.emailAddress);
            email.Subject = "QR Code";

            //Attach QR code image to email
            Byte[] imgBytes = Convert.FromBase64String(QRImageSrc);
            MemoryStream stream = new MemoryStream(imgBytes);
            Attachment attachment = new Attachment(stream, "qr_code.png");
            attachment.ContentId = "qr_code.png";
            attachment.ContentDisposition.Inline= true;

            email.Attachments.Add(attachment);
            email.Body = form.emailBody;

            smtpServer.Timeout = 10000;
            smtpServer.EnableSsl= true;
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Credentials = new NetworkCredential("QR.Gen.Project@gmail.com", ConfigurationManager.AppSettings.Get("GmailAppPassword"));
            smtpServer.Send(email);

            //show something to show sending succeeded
            ModelState.Clear();
            return View("Index");
        }
    }
}