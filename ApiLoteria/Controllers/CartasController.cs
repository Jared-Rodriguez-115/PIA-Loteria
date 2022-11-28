using ApiLoteria.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("api/cartas")]
    public class CartasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CartasController (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /*[HttpPost]

        public async Task<ActionResult> Post(Cartas carta)
        {
            dbContext.Add(carta);
            await dbContext.SaveChangesAsync();
            return Ok();
        }*/
    }
}
