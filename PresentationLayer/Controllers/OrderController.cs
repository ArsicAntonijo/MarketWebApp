using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System;

namespace PresentationLayer.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public OrderController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            SetTypeBag();
            return View();
        }
        public IActionResult Create()
        {
            SetTypeBag();
            return View();
        }
        public IActionResult Edit(int id)
        {
            SetTypeBag();
            ViewBag.Id = id;
            return View();
        }
        public IActionResult Delete(int id)
        {
            SetTypeBag();
            ViewBag.Id = id;
            return View();
        }
        public IActionResult Details(int id)
        {
            SetTypeBag();
            ViewBag.Id = id;
            return View();
        }

        public IActionResult ConfirmOrder(int id)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:7122/api/Sales/Order");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "PUT";
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            StringBuilder sb = new StringBuilder();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //Body context 
                string json = JsonConvert.SerializeObject(new
                {
                    orderId = id,
                    confirmed = "true"
                });

                streamWriter.Write(json);
            }

            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        private void SetTypeBag()
        {
            ViewBag.Type = HttpContext.Session.GetString("type");
        }
    }
}
