using Entities.Models.BasicInformation;

namespace ParsCenterApi.Models.BasicInformation
{
    public class CityDto
    {
        public string Title { get; set; }

        public int StateId { get; set; } //State.Id

        public string StateTitle { get; set; } //State.Title
    }
}
