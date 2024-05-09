using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DesafioAPI.Core.Modelos
{
    public class Pessoa
    {
        public Pessoa() {}

        public Pessoa(int codigo, string nome, string cpf, string uf, DateTime dataDeNascimento)
        {
            Codigo = codigo;
            Nome = nome;
            CPF = cpf;
            UF = uf;
            DataDeNascimentoValue = dataDeNascimento;
        }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório."), MaxLength(100, ErrorMessage = "Máximo de 100 caracteres."), MinLength(3, ErrorMessage = "Minímo de 3 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório."), MaxLength(14, ErrorMessage = "Máximo de 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória."), MaxLength(2, ErrorMessage = "Máximo de 2 caracteres.")]
        public string UF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatório.")]
        public DateTime? DataDeNascimentoValue { get; set; }

        public string DataDeNascimento => DataDeNascimentoValue?.ToString("dd/MM/yyyy");
    }
}
