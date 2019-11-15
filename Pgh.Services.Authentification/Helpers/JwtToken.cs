using System;
using System.IdentityModel.Tokens.Jwt;

namespace Pgh.Services.Authentification.Helpers
{
    public sealed class JwtToken
    {
        private JwtSecurityToken _token;

        internal JwtToken(JwtSecurityToken token)
        {
            _token = token;
        }

        public DateTime ValidTo => _token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(this._token);
    }
}
