using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Chapter_02_Certificates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Chapter_02_Certificates.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private static Lazy<X509SigningCredentials> SigningCredentials;
    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      var SigningCertThumbprint = "9FBD99EAAF6CB8851643E2733B3D8125AB484298";
      SigningCredentials = new Lazy<X509SigningCredentials>(() =>
      {
        X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        certStore.Open(OpenFlags.ReadOnly);
        X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, SigningCertThumbprint, false);
        // Get the first cert with the thumbprint
        if (certCollection.Count > 0)
        {
          return new X509SigningCredentials(certCollection[0]);
        }
        throw new Exception("Certificate not found.");
      });
      var myCert = SigningCredentials.Value;
      ViewBag.myCertThumbprint = myCert.Certificate.Thumbprint.ToString();
      ViewBag.myCertSubject = myCert.Certificate.SubjectName.Name.ToString();
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
