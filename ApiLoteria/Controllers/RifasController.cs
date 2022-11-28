using ApiLoteria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiLoteria.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("api/rifas")]

    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public RifasController(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        [HttpGet("/consultarRifas")]// api/rifas

        public async Task<ActionResult<List<GetRifaDTO>>> Get()
        {
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifaDTO>>(rifas);

        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<GetRifaDTO>> Get(int id)
        //{
        //var rifa = await dbContext.Rifas.FirstOrDefaultAsync(rifaBD => rifaBD.Id == id);

        //if (rifa == null)
        //{
        //return NotFound("No hemos encontrado esa rifa");
        //}
        //return mapper.Map<GetRifaDTO>(rifa);

        //}


        [HttpGet("{id:int}", Name = "obtenerrifa")] //{id}/rifa
        public async Task<ActionResult<RifaDTOConCartas>> Get(int id)
        {
           var rifa = await dbContext.Rifas
                 .Include(rifaDB => rifaDB.RPCP)
                 .ThenInclude(RPCPDB  => RPCPDB.Cartas)
                 .FirstOrDefaultAsync(rifaBD => rifaBD.Id == id);

            if (rifa == null)
            {
                return NotFound();
            }

            return mapper.Map<RifaDTOConCartas>(rifa);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetRifaDTO>>> Get([FromRoute] string nombre)
        {
            var rifas = await dbContext.Rifas.Where(rifaBD => rifaBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetRifaDTO>>(rifas);

        }


        [HttpPost]
        
        public async Task<ActionResult> Post(RifaCreacionDTO rifaCreacionDTO)
        {
            var existeRifaMismoNombre = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifaCreacionDTO.Nombre);

            if (existeRifaMismoNombre)
            {
                return BadRequest($"Ya existe una rifa con el nombre {rifaCreacionDTO.Nombre}");
            }

            var rifa = mapper.Map<Rifa>(rifaCreacionDTO);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();

            var rifaDTO = mapper.Map<GetRifaDTO>(rifa);

            return CreatedAtRoute("obtenerrifa", new { id = rifa.Id }, rifaDTO);
        }


        [HttpPut("{id:int}")] // api/rifas/id
        
        public async Task<ActionResult> Put(RifaCreacionDTO rifaCreacionDTO, int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            var rifa = mapper.Map<Rifa>(rifaCreacionDTO);
            rifa.Id = id;

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("La rifa ha sido eliminada");
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
