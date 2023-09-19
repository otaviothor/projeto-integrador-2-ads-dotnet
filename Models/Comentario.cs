using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Conteudo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuarioFk { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Postagem")]
        public int IdPostagemFk { get; set; }
        public Postagem Postagem { get; set; }
    }
}