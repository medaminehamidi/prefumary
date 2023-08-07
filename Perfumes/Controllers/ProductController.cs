using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Perfumes.Models;
using PostgreSQL.Models;
using PostgreSQL.Products;
using Services;
using System.Data;

namespace Perfumes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private IReadProduct _readProduct;
        private IReadPromo _readPromo;
        private IWriteProduct _writeProduct;
        private IProductWithPromo _productWithPromo;
        public ProductController(IReadProduct readProduct, IWriteProduct writeProduct, IReadPromo readPromo, IProductWithPromo productWithPromo)
        {
            _readProduct = readProduct;
            _readPromo = readPromo;
            _writeProduct = writeProduct;
            _productWithPromo = productWithPromo;
        }

        [HttpGet]
        public async Task<IEnumerable<PromoProductDto>> Get()
        {
            var result = await _productWithPromo.GetProductsWithPromo();
            return result.Select(product => new PromoProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsInStock = product.IsInStock,
                PromoPrice = product.PromoPrice
            });
        }

        [HttpGet("{id}")]
        public async Task<ProductPromoDto> GetProductById(Guid id)
        {
            var result = await _readProduct.GetProduct(id);

            var promo = await _readPromo.GetPromo(id);
            var promoPrice = 0;
            if (promo != null && result.Price != null) promoPrice = (int)(result.Price * promo.Percentage / 100);
            return new ProductPromoDto
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                IsInStock = result.IsInStock,
                PromoPrice = (int)(result.Price - promoPrice),
                PromoName = promo.Name
            };
        }

        [HttpGet("isInStock/{id}")]
        public async Task<bool> GetIsProductInStock(Guid id)
        {
            var result = await _readProduct.IsProductInStock(id);
            return result;
        }

        [HttpPost]
        public async Task<OkResult> Post(NewProductDto product)
        {
            await _writeProduct.AddProduct(new NewProduct
            {
                Name = product.Name,
                Price = product.Price,
                IsInStock = product.IsInStock
            });
            return Ok();
        }

        [HttpPut]
        public async Task<OkResult> Put(ProductDto product)
        {
            await _writeProduct.UpdateProduct(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsInStock = product.IsInStock
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(Guid id)
        {
            await _writeProduct.DeleteProduct(id);
            return Ok();
        }
    }
}
