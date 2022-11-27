using ApiLoteria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiLoteria.DTOs;


namespace ApiLoteria.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    public class RifasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RifasController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/consultarRifas")]// api/rifas
        public async Task<ActionResult<List<GetRifaDTO>>> Get()
        {
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifaDTO>>(rifas);

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetRifaDTO>> Get(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(rifaBD => rifaBD.Id == id);

            if(rifa == null)
            {
                return NotFound("No hemos encontrado esa rifa");
            }
            return mapper.Map<GetRifaDTO>(rifa);

        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetRifaDTO>>> Get(string nombre)
        {
            var rifas = await dbContext.Rifas.Where(rifaBD => rifaBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetRifaDTO>>(rifas);

        }


        [HttpPost]
        
        public async Task<ActionResult> Post(RifaDTO rifaDTO)
        {
            var existeRifaMismoNombre = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifaDTO.Nombre);

            if (existeRifaMismoNombre)
            {
                return BadRequest($"Ya existe una rifa con el nombre {rifaDTO.Nombre}");
            }

            var rifa = mapper.Map<Rifa>(rifaDTO);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")] // api/rifas/id
        
        public async Task<ActionResult> Put(Rifa rifa, int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            if (rifa.Id != id)
            {
                return BadRequest("El id de la rifa no coincide con el establecido con el establecido en la url");
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
