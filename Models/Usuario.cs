using System.ComponentModel.DataAnnotations;

namespace ProjetoInterdisciplinarII.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [StringLength(3)]
        public string Nivel { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
    }

}