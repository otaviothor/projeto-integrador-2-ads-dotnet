using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoInterdisciplinarII.Models
{
    public class Postagem
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Imagem { get; set; }

        [Column(TypeName = "text")]
        public string Conteudo { get; set; }

        [Column(TypeName = "int"), DefaultValue(1)]
        public string Ativo { get; set; }

        public List<Comentario> Comentarios { get; set; }
        public List<Curtida> Curtidas { get; set; }
    }
}