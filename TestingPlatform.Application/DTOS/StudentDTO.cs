using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestingPlatform.Domain.Enums;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.DTOS
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string VKLink { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int? EducationScore { get; set; }
        public int? AdditionalScore { get; set; }
        public int? OtherScore { get; set; }
    }
}
