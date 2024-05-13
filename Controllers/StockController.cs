using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{   [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //preventing to be mutable
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        // --------- retrieveing all stocks (removed comments by DTO) ----------- //
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _context.Stocks.Select(s => s.ToStockDTO()).ToListAsync();
            return Ok(stocks);
        }   

        // -------- retrieveing stock by id -------------- //
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null){
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        // ------------ inserting sticks into table ----------------- //
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockPostDTO stockDTO){

            var stockModel = stockDTO.ToStockPostDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            // return Ok(stock);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());

        }      

        // ------------ updating sticks into table ----------------- //
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockPutDTO stockDTO){

            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null){
                return NotFound();
            }

            stockModel.Symbol = stockDTO.Symbol;
            stockModel.CompanyName = stockDTO.CompanyName;
            stockModel.Purchase = stockDTO.Purchase;
            stockModel.LastDiv = stockDTO.LastDiv;
            stockModel.Industry = stockDTO.Industry;
            stockModel.MarketCap = stockDTO.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDTO());
        }

        // ------------ deleting sticks into table ----------------- //

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id){
            
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null){
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}