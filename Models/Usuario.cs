using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Login { get; set; }

        [Column(TypeName = "varchar(3)")]
        public string Nivel { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Senha { get; set; }

        public List<Postagem> Postagens { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Curtida> Curtidas { get; set; }
    }
}