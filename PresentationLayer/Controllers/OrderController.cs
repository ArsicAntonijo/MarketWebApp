using Microsoft.AspNetCore.Mvc;

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

        private void SetTypeBag()
        {
            ViewBag.Type = HttpContext.Session.GetString("type");
        }
    }
}
