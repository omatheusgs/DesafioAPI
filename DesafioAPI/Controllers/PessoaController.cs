using DesafioAPI.Core.Modelos;
using DesafioAPI.Core.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.Web.Controllers
{
    /// <summary>
    /// Rotas relacionadas as ações da <see cref="Pessoa"/>.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("v1/pessoa")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public class PessoaController : Controller
    {
        private readonly IPessoaServico _pessoaServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="pessoaServico"></param>
        public PessoaController(IPessoaServico pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        /// <summary>
        /// Consulta uma pessoa com o código informado.
        /// </summary>
        /// <param name="codigo">Código da pessoa.</param>
        /// <returns>A pessoa no cache. Veja: <see cref="Pessoa">.</returns>
        [HttpGet("ConsultePessoaPorCodigo/{codigo}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorCodigo([FromRoute] int codigo)
        {
            var resposta = _pessoaServico.ConsultePessoaPorCodigo(codigo);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Consulta uma lista de pessoas que residem na UF informada.
        /// </summary>
        /// <param name="uf">UF da pessoa.</param>
        /// <returns>A lista de pessoas no cache para a UF informada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("ConsultePessoaPorUF/{uf}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultePessoaPorUF([FromRoute] string uf)
        {
            var resposta = _pessoaServico.ConsultePessoasPorUF(uf);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Consulta a lista de pessoas.
        /// </summary>
        /// <returns>A lista de pessoas no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("ConsulteTodasAsPessoas")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsulteTodasAsPessoas()
        {
            var resposta = _pessoaServico.ConsulteTodasAsPessoas();
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Adiciona o cadastro da pessoa informada no cache.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser cadastrada.</param>
        /// <returns>A pessoa que foi adicionada no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPost("AdicionarPessoa")]
        [ProducesResponseType(typeof(Pessoa), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            var resposta = _pessoaServico.AdicionarPessoa(pessoa);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Atualiza o cadastro de uma pessoa, tendo seu código como base.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa para ser atualizada.</param>
        /// <returns>Os dados da pessoa atualizada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPut("AtualizarPessoa")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AtualizarPessoa([FromBody] Pessoa pessoa)
        {
            var resposta = _pessoaServico.AtualizarPessoa(pessoa);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }

        /// <summary>
        /// Exclui o cadastro de uma pessoa do cache.
        /// </summary>
        /// <param name="codigo">Código da pessoa a ser excluída do cache.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpDelete("ExcluirPessoa/{codigo}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ExcluirPessoa([FromRoute] int codigo)
        {
            var resposta = _pessoaServico.ExcluirPessoa(codigo);
            return StatusCode(resposta.CodigoHTTP, resposta.Resultado);
        }
    }
}
