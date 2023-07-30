using Entities.Models.BasicInformation;

namespace ParsCenterApi.Models.BasicInformation
{
    public class CountryDto
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public string TitleEn { get; set; }

        public ICollection<State> States { get; set; }
    }
}
