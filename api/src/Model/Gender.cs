using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.src.Model
{
    public class Gender
    {
        public int Id { get; set; }
        [RegularExpression(@"FEMENINO|MASCULINO|OTRO|PREFIERO NO DECIRLO")]
        public required string GenderName { get; set; }

        public ICollection<User> Users = new List<User>();
    }
}