using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFood.Infrastructure
{
    public interface ITokenGenerator
    {
        public string GenerateJwtToken( string email, string secretKey);
    }
}
