using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.Core.Modelos
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(int codigo, string login, string senha)
        {
            Codigo = codigo;
            Login = login;
            Senha = senha;
        }

        public int Codigo { get; set; }

        [Required, MaxLength(50)]
        public string Login { get; set; }
        [Required, MaxLength(50)]
        public string Senha { get; set; }

        public string Token { get; set; }
    }
}
