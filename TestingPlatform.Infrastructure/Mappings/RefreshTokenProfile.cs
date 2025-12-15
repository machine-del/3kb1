using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Infrastructure.Data;

namespace TestingPlatform.Infrastructure.Mappings
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        }
    }
}
