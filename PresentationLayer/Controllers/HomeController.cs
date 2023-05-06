using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Collections.Specialized;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //HttpContext.Session.SetString("id", "1");
            //ViewBag.Id = HttpContext.Session.GetString("id");
            return View();
        }

        public IActionResult Item()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult LogIn() 
        {
            return View();
        }

        public IActionResult ProcessLogIn(string Email, string Password) 
        {
            bool valid = false;
            string result = string.Empty;
            Regex reg = new Regex("\\\"(?<id>[0-9]+)\\\"");

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:7122/api/Sales/Customer/Log");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            StringBuilder sb = new StringBuilder();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //Body context 
                string json = JsonConvert.SerializeObject(new
                {
                    email = Email,
                    password = Password
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

                //add regex here
                valid = reg.Match(result).Success;
            }
            catch (Exception ex)
            {
            }
            if (valid)
            {
                string id = reg.Match((string)result).Groups["id"].Value;
                HttpContext.Session.SetString("CustomerId", id);
                HttpContext.Session.SetString("type", "valid");
                return RedirectToAction("Index");
            }
            // ViewBag.Id = HttpContext.Session.GetString("id");
            return RedirectToAction("LogIn");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("CustomerId", "");
            HttpContext.Session.SetString("type", "");
            return Redirect("Item");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}