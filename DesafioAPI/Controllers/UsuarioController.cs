using DesafioAPI.Core.Modelos;
using DesafioAPI.Core.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Web.Controllers
{
    /// <summary>
    /// Rotas relacionadas as ações do <see cref="Usuario"/>.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("v1/Usuario")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServico _usuarioServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="usuarioServico"></param>
        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        /// <summary>
        /// Verifica se existe um usuário cadastrado com o acesso fornecido e, se sim, retorna seus dados, juntamente com um token de autenticação.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para acesso.</param>
        /// <returns>Dados do usuário encontrado.</returns>
        [HttpPost("AutentiqueUsuario")]
        [ProducesResponseType(typeof(Usuario), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AutentiqueUsuario([FromBody] CredenciaisDeAcesso credenciais)
        {
            var resposta = _usuarioServico.ConsulteUsuarioPorLoginESenha(credenciais);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Recebe as credenciais informadas e cadastra um novo usuário com o acesso informado.
        /// </summary>
        /// <param name="credenciais">Usuário e senha para acesso.</param>
        /// <returns>Dados do usuário cadastrado.</returns>
        [HttpPost("AdicionarNovoUsuario")]
        [ProducesResponseType(typeof(Usuario), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult AdicionarNovoUsuario([FromBody] CredenciaisDeAcesso credenciais)
        {
            var resposta = _usuarioServico.AdicionarUsuario(credenciais);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }
    }
}
