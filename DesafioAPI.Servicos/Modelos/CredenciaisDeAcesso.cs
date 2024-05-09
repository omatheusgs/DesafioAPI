using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Core.Modelos
{
    public class CredenciaisDeAcesso
    {
        [Required(ErrorMessage = "Usuário é obrigatório.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
