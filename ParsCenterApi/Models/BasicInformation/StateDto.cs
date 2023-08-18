using Entities.Models.BasicInformation;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace ParsCenterApi.Models.BasicInformation
{
    public class StateDto : BaseDto<StateDto, State, int>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int CountryId { get; set; }

        public string CountryTitle { get; set; } // Country.Title

        //public ICollection<City> Cites { get; set; }
    }
}
