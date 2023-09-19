using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Curtida
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Imagem { get; set; }

        [Column(TypeName = "text")]
        public string Conteudo { get; set; }

        public Usuario Usuario { get; set; }
    }
}