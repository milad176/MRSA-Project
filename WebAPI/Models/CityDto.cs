using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a mandatory field")]
        [StringLength(50, MinimumLength =2)]
        [RegularExpression(".*[a-z]", ErrorMessage = "Only alphabets are allowed for Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country is a mandatory field")]
        [RegularExpression(".*[a-z]", ErrorMessage = "Only alphabets are allowed for Country")]
        public string Country { get; set; }
    }
}
