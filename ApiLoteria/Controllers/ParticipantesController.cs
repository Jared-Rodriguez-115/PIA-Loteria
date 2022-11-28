using ApiLoteria.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiLoteria.DTOs;

namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("participantes")]

    public class ParticipantesController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ParticipantesController (ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoParticipantes")]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            return await dbContext.Participantes.ToListAsync();
        }


        [HttpGet("{id:int}", Name = "obtenerParticipante")]
        public async Task<ActionResult<ParticipanteDTOConRifas>> GetById(int id)
        {
            var participante = await dbContext.Participantes
              .Include(participanteDB => participanteDB.RPCP)
              .ThenInclude(RPCPDB => RPCPDB.Rifa)
              .Include(cartaDB => cartaDB.Cartas)
              .FirstOrDefaultAsync(x => x.Id == id);

            if (participante == null)
            {
                return NotFound();
            }
            participante.RPCP = participante.RPCP.OrderBy(x => x.Orden).ToList();
            return mapper.Map<ParticipanteDTOConRifas>(participante);
        }


        [HttpPost]
        public async Task<ActionResult> Post(ParticipanteCreacionDTO participanteCreacionDTO)
        {
            if ( participanteCreacionDTO.RifasIds == null)
            {
                return BadRequest("No se puede crear un participante sin rifa.");
            }

            var rifasIds = await dbContext.Rifas
                .Where(rifaBD => participanteCreacionDTO.RifasIds.Contains(rifaBD.Id)).
                Select(x => x.Id).ToListAsync();

            if (participanteCreacionDTO.RifasIds.Count != rifasIds.Count)
            {
                return BadRequest("No existe uno de las rifas enviadas");
            }

            if (participanteCreacionDTO.CartasIds == null)
            {
                return BadRequest("No se puede crear un participante sin carta.");
            }
                                                         
            var cartasIds = await dbContext.Cartas 
                .Where(cartaBD => participanteCreacionDTO.CartasIds.Contains(cartaBD.Id)).
                Select(x => x.Id).ToListAsync();

            if (participanteCreacionDTO.CartasIds.Count != cartasIds.Count)
            {
                return BadRequest("No existe uno de las cartas enviadas");
            }

            var participante = mapper.Map<Participante>(participanteCreacionDTO);

            //OrdenarPorRifas(participante);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();

            var  participanteDTO = mapper.Map<ParticipanteDTO>(participante);

            return CreatedAtRoute("obtenerParticipante", new { id = participante.Id }, participanteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ParticipanteCreacionDTO participanteCreacionDTO, int id)
        {
            var participanteDB = await dbContext.Participantes
                  .Include(x => x.RPCP)
                  .FirstOrDefaultAsync(x => x.Id == id);

            if (participanteDB == null)
            {
                return NotFound();
            }

            participanteDB  = mapper.Map(participanteCreacionDTO, participanteDB);

            OrdenarPorRifas(participanteDB);

            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no se ha encontrado");
            }

            dbContext.Remove(new Participante { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();

        }
        private void OrdenarPorRifas(Participante participante)
        {
            if (participante.RPCP != null)
            {
                for (int i = 0; i < participante.RPCP.Count;i++) 
                {
                    participante.RPCP[i].Orden = i;
                }
            }
        }

        //[HttpPatch("{id:int}")]
        //public async Task<ActionResult> Patch(int id,
             //JsonPatchDocument<ParticipantePatchDTO> patchDocument)
        //{
            //if (patchDocument == null) { return BadRequest(); }

            //var participanteDB = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);

            //if (participanteDB == null) { return NotFound(); }

            //var participanteDB = mapper.Map<ParticipantePatchDTO>(participanteDB);

            //patchDocument.ApplyTo(participanteDB);

            //var isValid = TryValidateModel(participanteDB);

            //if (!isValid)
            //{
               // return BadRequest(ModelState);
            //}

            //mapper.Map(participanteDB, participanteDB);

            //await dbContext.SaveChangesAsync();
            //return NoContent();
        //}
    }

}
