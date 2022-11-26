using ApiLoteria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    public class RifasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RifasController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rifa>>> Get()
        {
            return await dbContext.Rifas.ToListAsync();

        }


        [HttpPost]

        public async Task<ActionResult> Post(Rifa rifa)
        {
            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")] // api/rifas/1
        public async Task<ActionResult> Put(Rifa rifa, int id)
        {

            if (rifa.Id != id)
            {
                return BadRequest("El id de la rifa no coincide con el establecido en la url.");
            }

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("La rifa no fue encontada");
            }
            dbContext.Remove(new Rifa()
            {
                Id = id

            });
            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
