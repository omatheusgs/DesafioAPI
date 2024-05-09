using DesafioAPI.Core.Modelos;

namespace DesafioAPI.Core.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        /// <summary>
        /// Valida os dados informados e adiciona um novo usuário na aplicação.
        /// </summary>
        /// <param name="credenciais">Credenciais para a criação de um novo usuário. Composto por usuário e senha.</param>
        /// <returns>Os dados do usuário que foi persistido. Veja <see cref="RespostaDaRequisicao"/> e <see cref="Usuario"/>.</returns>
        RespostaDaRequisicao AdicionarUsuario(CredenciaisDeAcesso credenciais);

        /// <summary>
        /// Consulta um usuário.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para autenticação.</param>
        /// <returns>Os dados do usuário persistido, juntamente com um token de autenticação para a API. Veja <see cref="RespostaDaRequisicao"/> e <see cref="Usuario"/>.</returns>
        RespostaDaRequisicao ConsulteUsuarioPorLoginESenha(CredenciaisDeAcesso credenciais);
    }
}
