using Microsoft.EntityFrameworkCore;

namespace ProjetoInterdisciplinarII.Models.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Postagens)
                .HasForeignKey(p => p.IdUsuarioFk);

            modelBuilder.Entity<Curtida>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Curtidas)
                .HasForeignKey(c => c.IdUsuarioFk);

            modelBuilder.Entity<Curtida>()
                .HasOne(c => c.Postagem)
                .WithMany(p => p.Curtidas)
                .HasForeignKey(c => c.IdPostagemFk);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.IdUsuarioFk);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Postagem)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.IdPostagemFk);
        }

    }
}