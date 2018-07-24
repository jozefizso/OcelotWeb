using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OcelotWeb.Models;

namespace OcelotWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ////var start = DateTimeOffset.UtcNow;
            ////var end = DateTimeOffset.UtcNow.AddYears(5);

            ////var cm = new CertificatesManager();
            ////var certificate = cm.CreateAspNetCoreHttpsDevelopmentCertificate(start, end, "CN=ACME Certification Authority,O=ACME,C=SK", "ACME Certification Authority");

            ////var pem = certificate.ToPem();
            ////var model = new CertificateInfo()
            ////{
            ////    SubjectName = certificate.SubjectName.Name,
            ////    FriendlyName = certificate.FriendlyName,
            ////    PEM = pem
            ////};

            ////return View(model);

            return RedirectToAction(nameof(GenerateCA));
        }

        [HttpGet]
        public IActionResult GenerateCA()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateCA(CreateCertificationAuthorityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var subjectName = $"CN={model.FriendlyName}";

            var start = DateTimeOffset.UtcNow;
            var end = DateTimeOffset.UtcNow.AddYears(model.ValidityYears);

            var cm = new CertificatesManager();
            var certificate = cm.CreateCertificationAuthorityCertificate(start, end, subjectName, model.FriendlyName);

            var pem = certificate.ToPem();

            return Ok(pem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return base.View(model);
        }
    }
}
