using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Mappers
{   //because this will be extension methods we put static
    public static class StocksMapper
    {   
        // ----------- this mapper is for returning all Stock fot GET rewuest ----------- //
        public static StocksDTOs ToStockDTO(this Stocks stocksDTOs){

            return new StocksDTOs{

            Id = stocksDTOs.Id,
            Symbol = stocksDTOs.Symbol,
            CompanyName = stocksDTOs.CompanyName,
            Purchase = stocksDTOs.Purchase,
            LastDiv = stocksDTOs.LastDiv,
            Industry = stocksDTOs.Industry,
            MarketCap = stocksDTOs.MarketCap,
        };
        }

        // ---------- this mapper is for inserting new Stock for POST rewuest ---------- //
        public static Stocks ToStockPostDTO(this StockPostDTO stocksDTOs){

            return new Stocks{

              Symbol = stocksDTOs.Symbol,
              CompanyName = stocksDTOs.CompanyName,
              Purchase = stocksDTOs.Purchase,
              LastDiv = stocksDTOs.LastDiv,
              Industry = stocksDTOs.Industry,
              MarketCap = stocksDTOs.MarketCap  
            };
        }
    }
}