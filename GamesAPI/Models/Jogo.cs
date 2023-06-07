using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GamesAPI.Models
{
    [Table("Jogos")]
    public class Jogo
    {
        [Key]
        public int JogoId { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Nome { get; set; }
        [Required]
        [MaxLength(300)]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName = "decimal(3,2)")]
        public decimal? Nota { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(300)]
        public string? ImagemUrl { get; set; }
        [Required]
        [MaxLength(4)]
        public string? AnoLancamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
