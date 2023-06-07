using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesAPI.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public Categoria()
        {
            Jogos = new Collection<Jogo>();
        }
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string? ImagemUrl { get; set; }
        public ICollection<Jogo>? Jogos { get; set;}
    }
}
