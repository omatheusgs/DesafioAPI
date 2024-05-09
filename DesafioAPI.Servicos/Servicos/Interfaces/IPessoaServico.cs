using DesafioAPI.Core.Modelos;

namespace DesafioAPI.Core.Servicos.Interfaces
{
    public interface IPessoaServico
    {
        /// <summary>
        /// Consulta os dados de uma pessoa pelo seu código.
        /// </summary>
        /// <param name="codigoPessoa">Código para consulta.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> (se existir).</returns>
        RespostaDaRequisicao ConsultePessoaPorCodigo(int codigoPessoa);

        /// <summary>
        /// Consulta os dados das pessoas através da UF fornecida.
        /// </summary>
        /// <param name="uf">UF para consulta. Ex: GO e TO.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        RespostaDaRequisicao ConsultePessoasPorUF(string uf);

        /// <summary>
        /// Consulta todos os cadastros de pessoas.
        /// </summary>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        RespostaDaRequisicao ConsulteTodasAsPessoas();

        /// <summary>
        /// Grava os dados de uma nova pessoa.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser persistida.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> informada.</returns>
        RespostaDaRequisicao AdicionarPessoa(Pessoa pessoa);

        /// <summary>
        /// Atualiza o cadastro de uma pessoa.
        /// </summary>
        /// <param name="pessoa">Pessoa a ser atualizada.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> atualizada.</returns>
        RespostaDaRequisicao AtualizarPessoa(Pessoa pessoa);

        /// <summary>
        /// Exclui os dados da pessoa informada.
        /// </summary>
        /// <param name="codigoPessoa">Código da pessoa a ser removida.</param>
        /// <returns>O código HTTP para a requisição, juntamente com uma mensagem informativa da ação.</returns>
        RespostaDaRequisicao ExcluirPessoa(int codigoPessoa);
    }
}
