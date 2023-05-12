using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Collections.Specialized;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

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
            SetTypeBag();
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
            SetTypeBag();
            return View();
        }

        public IActionResult Register()
        {
            SetTypeBag();
            return View();
        }

        public IActionResult LogIn() 
        {
            SetTypeBag();
            return View();
        }

        public IActionResult ProcessLogIn(string Email, string Password) 
        {
            bool valid = false;
            string result = string.Empty;
            Regex reg = new Regex("\\\"(?<id>[0-9]+)-(?<type>[a-z]*)\\\"");

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
                string type = reg.Match((string)result).Groups["type"].Value;
                HttpContext.Session.SetString("CustomerId", id);
                HttpContext.Session.SetString("type", type);
                return RedirectToAction("Index", "Item");
            }
            // ViewBag.Id = HttpContext.Session.GetString("id");
            return RedirectToAction("LogIn");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("CustomerId", "");
            HttpContext.Session.SetString("type", "");
            return RedirectToAction("Index", "Item");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetTypeBag()
        {
            ViewBag.Type = HttpContext.Session.GetString("type");
        }

        public IActionResult Test()
        {
            bool valid = false;
            string result = string.Empty;
            Regex reg = new Regex("\\\"(?<id>[0-9]+)-(?<type>[a-z]*)\\\"");

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:7122/api/Sales/Customer/Test");
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
                    email = "asd",
                    password = "asd"
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
            return RedirectToAction("Index");
        }
    }
}