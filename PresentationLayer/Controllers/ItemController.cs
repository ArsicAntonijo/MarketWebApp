﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Models;
using System.Net;
using System.Text;
using System;

namespace PresentationLayer.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ItemController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;   
            return View();
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public IActionResult Order(int id, string Name)
        {
            //if(string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerId")) {

            //}
            ViewBag.Name = Name;
            ViewBag.Id = id;
            return View();
        }

        public IActionResult NewOrderItem(string Id, string Name, string Amaunt)
        {
            CustomersOrder co;
            string? order = HttpContext.Session.GetString("order");
            if (string.IsNullOrEmpty(order))
            {
                co = new CustomersOrder("1");
            }
            else
            {
                co = JsonConvert.DeserializeObject<CustomersOrder>(order);
            }

            OrderedItem oi = new OrderedItem();
            oi.ItemId = Id;
            oi.Amaunt = Amaunt;
            oi.Name = Name;
            co.Items.Add(oi);

            HttpContext.Session.SetString("order", JsonConvert.SerializeObject(co));
            return RedirectToAction("Index");
        }

        public IActionResult ShoppingCart()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("order")))
            {
                List<OrderedItem> items = new List<OrderedItem>();
                ViewBag.Items = items;
            } 
            else
            {
                var co = JsonConvert.DeserializeObject<CustomersOrder>(HttpContext.Session.GetString("order"));
                ViewBag.Items = co.Items;
            }
            return View();
        }

        public IActionResult DeleteFromCart(string id)
        {
            var co = JsonConvert.DeserializeObject<CustomersOrder>(HttpContext.Session.GetString("order"));
            var item = co.Items.Where(i => i.ItemId == id).FirstOrDefault();
            co.Items.Remove(item);
            HttpContext.Session.SetString("order", JsonConvert.SerializeObject(co));
            return RedirectToAction("ShoppingCart");
        }

        public IActionResult OrderConfirmed()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("order")))
            {
                return RedirectToAction("Index");
            }
            var co = JsonConvert.DeserializeObject<CustomersOrder>(HttpContext.Session.GetString("order"));
            if (co != null || co.Items.Count > 0)
            {
                string str = "{\"confirmed\":\"false\",\"customer\":{\"id\":" + co.CustomerId + ",\"name\":\"string\",\"email\":\"string\",\"password\":\"string\",\"type\":\"string\"},\"orderedItems\":[";
                foreach (var item in co.Items)
                {
                    str += "{\"orderId\":0,\"itemId\":" + item.ItemId + ",\"amaunt\":" + item.Amaunt + "},";
                }
                str = str.Substring(0, str.Length - 1);
                str += "]}";
                string result = string.Empty;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:7122/api/Sales/Order");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                StringBuilder sb = new StringBuilder();

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    
                    ////Body context 
                    //string json = JsonConvert.SerializeObject(new
                    //{
                    //    customer = JsonConvert.SerializeObject(new
                    //    {
                    //        id = co.CustomerId
                    //    }),
                    //    orderedItems = "ajde"
                    //});

                    streamWriter.Write(str);
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
            }
            
            return RedirectToAction("Index");
        }
    }
}
