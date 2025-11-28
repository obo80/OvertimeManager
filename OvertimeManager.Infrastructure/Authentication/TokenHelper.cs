using OvertimeManager.Domain.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OvertimeManager.Infrastructure.Authentication
{
    public static class TokenHelper
    {
        public static bool IsEmployeeAuthorizedByEmail(string authorization, string email)
        {
            var authorizationEmail = GetUserEmailFromClaims(authorization);
            if (authorizationEmail != email)
            {
                return false;
            }
            return true;
        }
        public static bool IsEmployeeAuthorizedById(string authorization, int employeeId)
        {
            var authorizationEmployeeId = GetUserIdFromClaims(authorization);
            if (authorizationEmployeeId != employeeId)
            {
                return false;
            }
            return true;
        }

        public static int GetUserIdFromClaims(string authorization)
        {
            JwtSecurityToken token = GetTokenFromAuthorization(authorization);
            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
            {
                throw new BadRequestException("Invalid token");
            }
            return int.Parse(userIdClaim.Value);
        }

        public static string GetUserEmailFromClaims(string authorization)
        {
            JwtSecurityToken token = GetTokenFromAuthorization(authorization);
            var emailClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim is null)
            {
                throw new BadRequestException("Invalid token");
            }
            return emailClaim.Value;
        }

        private static JwtSecurityToken GetTokenFromAuthorization(string authorization)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = authorization.Replace("Bearer ", "");
            var token = handler.ReadJwtToken(jwt);
            return token;
        }
    }
}
