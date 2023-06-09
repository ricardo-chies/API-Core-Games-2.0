using GamesAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTO
{
    public class JogoDTO
    {
        public int JogoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal? Nota { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public string? AnoLancamento { get; set; }
        public int CategoriaId { get; set; }
    }
}
