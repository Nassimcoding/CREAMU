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
    public class ProductsDisplayAPIController : ControllerBase
    {
        private readonly CreamUdbContext _context;

        public ProductsDisplayAPIController(CreamUdbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsDisplayAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPageDisplayDTO>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var tempcontext_p = _context.Products;
            var tempp = await tempcontext_p.Where((e) => e.CategoryId >= 1 && e.CategoryId <= 7).Select(product => new ProductPageDisplayDTO
            {
                id = product.ProductId,
                name = product.ProductName,
                descript = product.Descript,
                cId = product.CategoryId,
                price = product.Price,
                productImage = product.ProductImage
            }).ToListAsync();
            return tempp;
        }

        // GET: api/ProductsDisplayAPI/text/5
        [HttpGet("text/{text}")]
        public async Task<ActionResult<IEnumerable<ProductPageDisplayDTO>>> GetProduct(string? text)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            if (text == "")
            {
                var tempcontext_p = _context.Products;
                var tempp = await tempcontext_p.Where((e) => e.CategoryId >= 1 && e.CategoryId <= 7).Select(product => new ProductPageDisplayDTO
                {
                    id = product.ProductId,
                    name = product.ProductName,
                    descript = product.Descript,
                    cId = product.CategoryId,
                    price = product.Price,
                    productImage = product.ProductImage
                }).ToListAsync();
                return tempp;
            }
            else
            {
                var tempcontext_p = _context.Products;
                var tempp = await tempcontext_p.Where((e) => e.CategoryId >= 1 && e.CategoryId <= 7
                && e.ProductName.Contains(text)
                || e.Descript.Contains(text)).Select(product => new ProductPageDisplayDTO
                {
                    id = product.ProductId,
                    name = product.ProductName,
                    descript = product.Descript,
                    cId = product.CategoryId,
                    price = product.Price,
                    productImage = product.ProductImage
                }).ToListAsync();
                return tempp;
            }
        }



        // GET: api/ProductsDisplayAPI/id/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ProductPageDisplayDTO>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var tempcontext_p = _context.Products;
            var tempp = await tempcontext_p.FirstOrDefaultAsync(e => e.ProductId == id);
            ProductPageDisplayDTO outputDTO = new ProductPageDisplayDTO
            {
                id = tempp.ProductId,
                name = tempp.ProductName,
                descript = tempp.Descript,
                cId = tempp.CategoryId,
                price = tempp.Price,
                productImage = tempp.ProductImage,
            };
            return outputDTO;

        }








        // PUT: api/ProductsDisplayAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsDisplayAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<String> PostProduct(ProductDetailToCart product)
        {
            //search product temporderdetail data
            var tempcontext_tod = _context.TempOrderDetails;
            var temp_tod_dto = await tempcontext_tod.Where((e) => e.MemberId == product.MemberId)
                .OrderBy((e) => e.OrderDetailId).ToListAsync();

            //search product price ... data
            var tempcontext_prod = _context.Products;
            var temp_prod_dto = tempcontext_prod.FirstOrDefault((e) => e.ProductId == product.ProductId);

            try
            {
                if (temp_tod_dto == null)
                {
                    //if don't have any data add temporderdetail data
                    //_context.TempOrderDetails.Add(new TempOrderDetail
                    //{
                    //    OrderDetailId = 1,
                    //    MemberId = product.MemberId,
                    //    ProductId = product.ProductId,
                    //    Qty = product.Qty,
                    //    UnitPrice = temp_prod_dto.Price,
                    //    Discount = null,
                    //    Subtotal = product.Qty * temp_prod_dto.Price,
                    //    Notes = null,
                    //    Type = "1",

                    //});
                    //wow almost forget add content

                    return "add data to cart fail";
                }
                else
                {
                    //if don't have any data add temporderdetail data
                    _context.TempOrderDetails.Add(new TempOrderDetail
                    {
                        //ODId don't forget add 1
                        OrderDetailId = temp_tod_dto.Max(e => e.OrderDetailId) + 1,
                        MemberId = product.MemberId,
                        ProductId = product.ProductId,
                        Qty = product.Qty,
                        UnitPrice = temp_prod_dto.Price,
                        Discount = null,
                        Subtotal = product.Qty * temp_prod_dto.Price,
                        Notes = null,
                        Type = "1",

                    });
                    //wow almost forget add content
                    await _context.SaveChangesAsync();
                    return "add data to cart success";
                }
            }
            catch (Exception ex)
            {
                return "add to cart fail";
            }
        }

        // DELETE: api/ProductsDisplayAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
