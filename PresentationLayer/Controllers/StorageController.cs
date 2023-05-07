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
            SetTypeBag();
            return View();
        }

        public IActionResult Create()
        {
            SetTypeBag();
            return View();
        }

        public IActionResult Details(string stringItems) 
        {
            SetTypeBag();
            List<ItemDetail> items = helper.ConvertToList(stringItems);
            ViewBag.Items = items;
            return View();
        }

        private void SetTypeBag()
        {
            ViewBag.Type = HttpContext.Session.GetString("type");
        }
    }
}
