using ApiLoteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiLoteria
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RPCP>()
                .HasKey(rc => new { rc.RifaId, rc.ParticipanteId, rc.CartasId, rc.PremioId });
        }

        public DbSet<Rifa> Rifas { get; set; }

        public DbSet<Participante> Participantes { get; set; }

        public DbSet<Cartas> Cartas { get; set; }

        public DbSet<Premio> Premios { get; set; }

        public DbSet<RPCP> RPCP { get; set; }

    }
}
