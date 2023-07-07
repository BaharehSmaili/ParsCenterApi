using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.basicInformation
{
    public class countryModel
    {
        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        [StringLength(100)]
        public string titleEn { get; set; }

        public ICollection<stateModel> states { get; set; }
    }
}
