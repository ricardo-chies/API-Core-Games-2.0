using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Jogo>? Jogos { get; set; }
    }
}
