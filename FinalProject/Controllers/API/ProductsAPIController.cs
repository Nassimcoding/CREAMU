using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Data.DTO;

namespace FinalProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public ProductsAPIController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsAPI/5
        [HttpGet("{productid}")]
        public async Task<ProductsDTO> GetProduct(int productid)
        {
            var product = await _context.Products.FindAsync(productid);  
            var ProductsDTO = new ProductsDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
            };

            return ProductsDTO;
        }

        
        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
