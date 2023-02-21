using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTF.Shopping.Context;
using OOTF.Shopping.Models;
using System.Data;

namespace OOTF.Shopping.Controllers
{
    [Route("/api/[controller]")]// для развертвывания контроллеров
    [ApiController] 
    public class ProductsController : Controller
    {
        private static List<Product> products = new List<Product>();

        [HttpGet]
        public IEnumerable<Product> GetProducts() => products;

        [HttpGet("GetById")]// параметр для маршрутизации
        public IActionResult Get(int id)
        {
            var product = products.SingleOrDefault(i => i.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("DeleteById")]
        [Authorize(Roles = "Manager")]
        public IActionResult Delete(int id)
        {
            products.Remove(products.SingleOrDefault(p => p.Id == id));
            return Ok(new { Message = "Deleted Successfully" }); 
        }

        private int NextProductId => 
            products.Count() == 0 ? 1 : products.Max(x => x.Id) + 1;

        [HttpGet("GetNextProductId")]
        [Authorize(Roles = "Manager")]
        public int GetNextProductId()
        {
            return NextProductId;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = NextProductId;
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "Manager")]
        public IActionResult PostBody([FromBody] Product product) =>
            Post(product);

        [HttpPut]
        public IActionResult Put(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedProduct = products.SingleOrDefault(p => p.Id == product.Id);
            if (storedProduct == null) return NotFound();
            storedProduct.Name = product.Name;
            storedProduct.Quantity = product.Quantity;
            storedProduct.Price = product.Price;
            storedProduct.Shops = product.Shops;
            storedProduct.ShopId= product.ShopId;
            return Ok(storedProduct);
        }

        [HttpPut("UpdateProducts")]
        public IActionResult PutBody([FromBody] Product product) =>
            Put(product);
    }
}
