using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Text.RegularExpressions;

namespace PresentationLayer.Controllers
{
    public class StorageController : Controller
    {
        ItemDetail helper = new ItemDetail();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(string stringItems) 
        {
            List<ItemDetail> items = helper.ConvertToList(stringItems);
            ViewBag.Items = items;
            return View();
        }
    }
}
