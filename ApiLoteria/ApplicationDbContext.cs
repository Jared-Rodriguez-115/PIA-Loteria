using ApiLoteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiLoteria
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Rifa> Rifas { get; set; }
    }
}
