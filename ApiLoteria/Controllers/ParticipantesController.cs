using ApiLoteria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ParticipantesController (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            return await dbContext.Participantes.ToListAsync();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Participante>> GetById(int id)
        {
            return await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);

        }

        [HttpPost]

        public async Task<ActionResult> Post(Participante participante)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(x => x.Id == participante.RifaId);

            if (!existeRifa)
            {
                return BadRequest($"No existe la rifa con el id: {participante.RifaId} ");
            }

            dbContext.Update(participante);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El participante no fue encontrado");
            }
            dbContext.Remove(new Participante
            {
                Id = id

            });
            await dbContext.SaveChangesAsync();
            return Ok();

        }



    }
}
