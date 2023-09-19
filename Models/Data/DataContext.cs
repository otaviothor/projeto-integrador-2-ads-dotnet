using Microsoft.EntityFrameworkCore;
using ProjetoInterdisciplinarII.Models;

namespace ProjetoInterdisciplinarII.Models.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}