using Entities.Models.BasicInformation;

namespace ParsCenterApi.Models.BasicInformation
{
    public class StateDto
    {
        public string Title { get; set; }

        public int CountryId { get; set; }

        public string CountryTitle { get; set; } // Country.Title

        //public ICollection<City> Cites { get; set; }
    }
}
