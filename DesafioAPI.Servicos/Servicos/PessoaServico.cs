using DesafioAPI.Core.Constantes;
using DesafioAPI.Core.Modelos;
using DesafioAPI.Core.Servicos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace DesafioAPI.Core.Servicos
{
    public class PessoaServico : IPessoaServico
    {
        /// <summary>
        /// Lista de pessoas salvas na memória da aplicação.
        /// </summary>
        public static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(1, "MATHEUS GOMES BRANDÃO", "700.056.340-09", "GO", new DateTime(1998, 09, 08))
        };

        public RespostaDaRequisicao ConsultePessoaPorCodigo(int codigoPessoa)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == codigoPessoa);
                if (pessoaPersistida == null)
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.RegistroNaoEncontrado);

                return new RespostaDaRequisicao(HttpStatusCode.OK, pessoaPersistida);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao ConsultePessoasPorUF(string uf)
        {
            try
            {
                var pessoas = Pessoas.Where(c => c.UF.ToUpper().Equals(uf.Trim().ToUpper()));
                if (pessoas == null || !pessoas.Any())
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.RegistroNaoEncontrado);

                return new RespostaDaRequisicao(HttpStatusCode.OK, pessoas);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao ConsulteTodasAsPessoas()
        {
            try
            {
                if (Pessoas == null || !Pessoas.Any())
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.RegistroNaoEncontrado);

                return new RespostaDaRequisicao(HttpStatusCode.OK, Pessoas);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao AdicionarPessoa(Pessoa pessoa)
        {
            try
            {
                if (Pessoas.Any(p => p.Codigo == pessoa.Codigo))
                    return new RespostaDaRequisicao(HttpStatusCode.Conflict, Mensagens.RegistroJaExistente);

                var errosDeValidacao = ObtenhaInformacoesInvalidasDePessoa(pessoa);
                if (errosDeValidacao.Any())
                    return new RespostaDaRequisicao(HttpStatusCode.BadRequest, errosDeValidacao);

                if (pessoa.Codigo == 0)
                    pessoa.Codigo = Pessoas.Max(c => c.Codigo) + 1;

                pessoa.CPF = FormatarCPF(pessoa.CPF);

                Pessoas.Add(pessoa);

                return new RespostaDaRequisicao(HttpStatusCode.Created, pessoa);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao AtualizarPessoa(Pessoa pessoa)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == pessoa.Codigo);
                if (pessoaPersistida == null)
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.RegistroNaoEncontrado);

                var errosDeValidacao = ObtenhaInformacoesInvalidasDePessoa(pessoa);
                if (errosDeValidacao.Any())
                    return new RespostaDaRequisicao(HttpStatusCode.BadRequest, errosDeValidacao);

                pessoaPersistida.UF = pessoa.UF;
                pessoaPersistida.CPF = FormatarCPF(pessoa.CPF);
                pessoaPersistida.DataDeNascimentoValue = pessoa.DataDeNascimentoValue;
                pessoaPersistida.Nome = pessoa.Nome;

                return new RespostaDaRequisicao(HttpStatusCode.OK, pessoaPersistida);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        public RespostaDaRequisicao ExcluirPessoa(int codigoPessoa)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(p => p.Codigo == codigoPessoa);
                if (pessoaPersistida == null)
                    return new RespostaDaRequisicao(HttpStatusCode.NotFound, Mensagens.RegistroNaoEncontrado);

                Pessoas.Remove(pessoaPersistida);

                return new RespostaDaRequisicao(HttpStatusCode.OK, Mensagens.RegistroExcluido);
            }
            catch (Exception)
            {
                return new RespostaDaRequisicao(HttpStatusCode.InternalServerError, Mensagens.ErroInterno);
            }
        }

        private Dictionary<string, string[]> ObtenhaInformacoesInvalidasDePessoa(Pessoa pessoa)
        {
            Dictionary<string, string[]> errosDeValidacao = new();

            if (!CPFValido(pessoa.CPF))
                errosDeValidacao.Add(nameof(pessoa.CPF), new string[] { "O CPF informado é inválido." });

            if (pessoa.DataDeNascimentoValue?.Date >= DateTime.Now.Date)
                errosDeValidacao.Add(nameof(pessoa.DataDeNascimento), new string[] { "A data de nascimento não pode ser maior que a data atual." });

            return errosDeValidacao;
        }

        private static bool CPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static string FormatarCPF(string cpf)
        {
            if (cpf.IsNullOrEmpty())
                return string.Empty;

            return Convert.ToUInt64(cpf.Replace(".", "").Replace("-", "")).ToString(@"000\.000\.000\-00");
        }
    }
}
