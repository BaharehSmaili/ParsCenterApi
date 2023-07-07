using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.basic;
using Entities.Models.basicInformation;

namespace Entities.Models.user
{
    public class userModel : baseEntity
    {
        [Required]
        [StringLength(100)]
        public string fName { get; set; }

        [Required]
        [StringLength(100)]
        public string lName { get; set; }

        [Required]
        [StringLength(10)]
        public string nationalCode { get; set; }

        [Required]
        [StringLength(11)]
        public string mobile { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string password { get; set; }

        public int fk_country { get; set; }
        public int fk_state { get; set; }
        public int fk_city { get; set; }

        [ForeignKey(nameof(fk_country))]
        public countryModel country { get; set; }

        [ForeignKey(nameof(fk_state))]
        public stateModel state { get; set; }

        [ForeignKey(nameof(fk_city))]
        public cityModel city { get; set; }

    }
}
