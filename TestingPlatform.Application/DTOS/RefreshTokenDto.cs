using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.DTOS
{
    public class RefreshTokenDto
    {
        public UserDTO User { get; set; }
        public DateTime Expires { get; set; }
    }
}
