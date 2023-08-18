using Entities.Models.BasicInformation;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace ParsCenterApi.Models.BasicInformation
{
    public class CityDto : BaseDto<CityDto, City, int>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int StateId { get; set; } //State.Id

        public string StateTitle { get; set; } //State.Title
    }
}
