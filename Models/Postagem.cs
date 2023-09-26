using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Postagem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(255)]
        public string Imagem { get; set; }

        [Required]
        [MaxLength]
        public string Conteudo { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Ativo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuarioFk { get; set; }
    }
}