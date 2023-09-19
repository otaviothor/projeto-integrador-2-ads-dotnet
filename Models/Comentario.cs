using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Conteudo { get; set; }

        public Usuario Usuario { get; set; }
    }
}