using Microsoft.AspNetCore.Mvc;
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

        // redirecting to view for amaunt selecting
        public IActionResult Order(int id, string Name)
        {
            SetTypeBag();
            //check here if customer is logged if not logged display alert
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerId"))) {
                ViewBag.DoAlert = "true";
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.Name = Name;
                ViewBag.Id = id;
                return View();
            }
            
        }

        // adding new ordered item in shopping cart
        public IActionResult NewOrderItem(string Id, string Name, string Amaunt)
        {
            CustomersOrder co;
            string? order = HttpContext.Session.GetString("order");
            if (string.IsNullOrEmpty(order))
            {
                string idd = HttpContext.Session.GetString("CustomerId");
                co = new CustomersOrder(idd);
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

        // displaying shopping cart items
        public IActionResult ShoppingCart()
        {
            SetTypeBag();
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

        //  removing item from shopping cart
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

        private void SetTypeBag()
        {
            ViewBag.Type = HttpContext.Session.GetString("type");
        }
    }
}
