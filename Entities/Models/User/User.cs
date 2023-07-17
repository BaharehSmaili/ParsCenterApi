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
    public class User : BaseEntity
    {
        public User()
        {
            IsActive = true;
            SecurityStamp = Guid.NewGuid();
        }

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
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset? LastLoginDate { get; set; }

        public Guid SecurityStamp { get; set; }

        //public int CountryId { get; set; }
        //public int StateId { get; set; }
        //public int CityId { get; set; }

        //[ForeignKey(nameof(CountryId))]
        //public Country Country { get; set; }

        //[ForeignKey(nameof(StateId))]
        //public State State { get; set; }

        //[ForeignKey(nameof(CityId))]
        //public City City { get; set; }

    }
}
