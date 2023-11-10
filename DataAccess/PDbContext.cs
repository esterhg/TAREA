using Microsoft.EntityFrameworkCore;
using TAREA1.Modelos;
using TAREA1.Utilidades;


namespace TAREA1.DataAccess
{
    public class PDbContext : DbContext
    {
        public DbSet<Personas> Personas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("Personas.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(col => col.IdPersonas);
                entity.Property(col => col.IdPersonas).IsRequired().ValueGeneratedOnAdd();
            });
        }


    }
}