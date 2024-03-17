using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.dto;

namespace UserServices
{
    public interface IAutnService
    {
        public Task<string> GetJwtTokenAsync(AuthDto auth);
    }
}
