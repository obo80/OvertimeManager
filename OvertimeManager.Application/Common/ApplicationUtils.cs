using OvertimeManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeManager.Application.Common
{
    public static class ApplicationUtils
    {
        public static int GetUserIdFromClaims(string authorization)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = authorization.Replace("Bearer ", "");
            var token = handler.ReadJwtToken(jwt);
            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null)
            {
                throw new BadRequestException("Invalid token");
            }
            return int.Parse(userIdClaim.Value);
        }
    }
}
