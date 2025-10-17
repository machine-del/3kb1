using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPlatform.Application.DTOS
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DirectionDTO Direction { get; set; }
        public CourseDTO Course { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
