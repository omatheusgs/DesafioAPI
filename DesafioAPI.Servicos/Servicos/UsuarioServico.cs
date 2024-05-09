using DesafioAPI.Core.Autenticacao;
using DesafioAPI.Core.Constantes;
using DesafioAPI.Core.Modelos;
using DesafioAPI.Core.Servicos.Interfaces;
using System.Net;

namespace DesafioAPI.Core.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        /// <summary>
        /// Lista de usuários salvos na aplicação. Utilizado somente para fins de teste.
        /// </summary>
        private static List<Usuario> Usuarios = new() { new Usuario(1, "admin", "admin") };

        public RespostaDaRequisicao AdicionarUsuario(CredenciaisDeAcesso credenciais)
        {
            try
            {
                if (Usuarios.Any(u => u.Login.Trim().ToUpper().Equals(credenciais.Usuario.Trim().ToUpper())))
                    return new RespostaDaRequisicao(HttpStatusCode.Conflict, "Esse usuário já está sendo utilizado.");

                var novoUsuario = new Usuario(Usuarios.Max(c => c.Codigo) + 1, credenciais.Usuario, credenciais.Senha);
                Usuarios.Add(novoUsuario);

                novoUsuario.Token = GerarToken.GerarTokenDeAutenticacao(novoUsuario.Login);

                return new RespostaDaRequisicao(HttpStatusCode.Created, novoUsuario);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao ConsulteUsuarioPorLoginESenha(CredenciaisDeAcesso credenciais)
        {
            try
            {
                var usuarioPersistido = Usuarios.FirstOrDefault(u => u.Login.Trim().ToUpper().Equals(credenciais.Usuario.Trim().ToUpper()) && u.Senha.Equals(credenciais.Senha));
                if (usuarioPersistido == null)
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.UsuarioOuSenhaIncorretos);

                if (usuarioPersistido != null)
                    usuarioPersistido.Token = GerarToken.GerarTokenDeAutenticacao(credenciais.Usuario);

                return new RespostaDaRequisicao(HttpStatusCode.OK, usuarioPersistido!);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }
    }
}
