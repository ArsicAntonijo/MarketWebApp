using AutoMapper;
using DataLayer.Dto;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private StorageService _service;
        private IMapper _mapper;

        public StorageController(StorageService service, IMapper mapper)
        {
            //            _service = new SalesService(context);
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public  async Task<ActionResult<IEnumerable<Receipt>>> GetAll() 
        {
            return Ok(await _service.GetAll());
        }

        //// GET: api/Items/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Receipt>> GetReceipt(int id)
        //{
        //    var receipt = await _service.GetReceipt(id);

        //    if (receipt == null)
        //    {
        //        return NotFound();
        //    }

        //    return receipt;
        //}

        [HttpPost]
        public async Task<IActionResult> PostReceipt(ReceiptDto rd)
        {
            var r = _mapper.Map<Receipt>(rd);
            _ = await _service.PostReceipt(r);
            return Ok();
        }

    }
}
