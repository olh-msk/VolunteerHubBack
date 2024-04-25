using Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services
{
    public class GetUserFromTokenService
    {
        private readonly IEnumerable<Claim> _claims;
        private int? _userId;
        private string? _email;
        private UserRole? _role;
        public int? UserId
        {
            get
            {
                if (_userId == null)
                {
                    _userId = int.TryParse(_claims.First(claim => claim.Type == "Id").Value, out int userId) ? userId : null;
                }
                return _userId;
            }
        }
        public string? Email
        {
            get
            {
                if (_email == null)
                {
                    _email = _claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                }
                return _email;
            }
        }
        public UserRole? Role
        {
            get
            {
                if (_role == null)
                {
                    _role = Enum.TryParse(_claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value, out UserRole role) ? role : null;
                }
                return _role;
            }
        }
        public GetUserFromTokenService(string? token)
        {
            _claims = new JwtSecurityTokenHandler()
                .ReadJwtToken(token)
                .Claims;
        }
    }
}
