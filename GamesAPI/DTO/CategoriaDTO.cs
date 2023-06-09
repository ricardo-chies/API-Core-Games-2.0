using GamesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTO
{
    public class CategoriaDTO
    {
        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? ImagemUrl { get; set; }
        public ICollection<Jogo>? Jogos { get; set; }
    }
}
