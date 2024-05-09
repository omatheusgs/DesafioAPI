using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioAPI.Core.Autenticacao
{
    public static class GerarToken
    {
        /// <summary>
        /// Cria um token de autenticação utilizando a chave de segurança como base.
        /// </summary>
        /// <param name="usuario">Usuário.</param>
        /// <returns>Token de autenticação. Bearer Token.</returns>
        public static string GerarTokenDeAutenticacao(string usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveParaGeracaoDeTokenDeAutenticacao = Encoding.ASCII.GetBytes(TokenDeAutenticacao);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveParaGeracaoDeTokenDeAutenticacao), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public const string TokenDeAutenticacao = "8o5g1456-4e0a-73tt-qo31-5217ac671041";
    }
}
