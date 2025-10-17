using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.DTOS;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IGroupRepository
    {
        List<GroupDTO> GetAll();
        GroupDTO GetById(int id);
        int Create (GroupDTO group);
        void Update(GroupDTO group);
        void Delete(int id);
    }
}
