using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Basic;
using Entities.Models.BasicInformation;

namespace Entities.Models.User
{
    public class UserModel : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Family { get; set; }

        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; }

        [Required]
        [StringLength(11)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public CountryModel Country { get; set; }

        [ForeignKey(nameof(StateId))]
        public StateModel State { get; set; }

        [ForeignKey(nameof(CityId))]
        public CityModel City { get; set; }

    }
}
