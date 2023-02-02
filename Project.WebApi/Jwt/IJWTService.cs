using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTO;
using Project.Framework.Api;

namespace Project.WebApi.Jwt
{
    public interface IJWTService
    {
        ApiResult GetToken(UserInfoDTO user);
    }
}
