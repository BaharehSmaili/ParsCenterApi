using Entities.Models.BasicInformation;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace ParsCenterApi.Models.BasicInformation
{
    public class CountryDto : BaseDto<CountryDto, Country, int>
    {
        //public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string TitleEn { get; set; }

        public ICollection<State> States { get; set; }
    }
}
