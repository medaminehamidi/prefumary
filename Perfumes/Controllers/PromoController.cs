using Microsoft.AspNetCore.Mvc;
using Perfumes.Models;
using PostgreSQL.Models;
using PostgreSQL.Products;

namespace Perfumes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoController : Controller
    {
        private IReadPromo _readPromo;
        private IWritePromo _writePromo;
        public PromoController(IReadPromo readPromo, IWritePromo writePromo)
        {
            _readPromo = readPromo;
            _writePromo = writePromo;
        }

        [HttpGet]
        public async Task<IEnumerable<PromoDto>> Get()
        {
            var result = await _readPromo.GetPromos();
            return result.Select(promo => new PromoDto
            {
                Id = promo.Id,
                Name = promo.Name,
                Percentage = promo.Percentage,
                StartDate = promo.StartDate,
                EndDate = promo.EndDate,
                ProductId = promo.ProductId
            });
        }

        [HttpPost]
        public async Task<OkResult> Post(NewPromoDto promo)
        {
            await _writePromo.AddPromo(new NewPromo
            {
                Name = promo.Name,
                Percentage = promo.Percentage,
                StartDate = promo.StartDate,
                EndDate = promo.EndDate,
                ProductId = promo.ProductId
            });
            return Ok();
        }

        [HttpPut]
        public async Task<OkResult> Put(PromoDto promo)
        {
            await _writePromo.UpdatePromo(new Promo
            {
                Id = promo.Id,
                Name = promo.Name,
                Percentage = promo.Percentage,
                StartDate = promo.StartDate,
                EndDate = promo.EndDate,
                ProductId = promo.ProductId
            });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<OkResult> Delete(Guid id)
        {
            await _writePromo.DeletePromo(id);
            return Ok();
        }
    }
}
