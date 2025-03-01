using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;    
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Stock> stocks = _context.Stocks.ToList();

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Stock? stock = _context.Stocks.Find(id);
            
            if (stock == null)
            {
                return NotFound();
            }
            
            return Ok(stock);
        }
    }
}
