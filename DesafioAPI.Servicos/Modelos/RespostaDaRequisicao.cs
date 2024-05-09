using System.Net;

namespace DesafioAPI.Core.Modelos
{
    public class RespostaDaRequisicao
    {
        public RespostaDaRequisicao(HttpStatusCode codigoHTTP, object resultado)
        {
            CodigoHTTP = (int)codigoHTTP;
            Resultado = resultado;
        }

        public int CodigoHTTP { get; set; }
        public object Resultado { get; set; }
    }
}
