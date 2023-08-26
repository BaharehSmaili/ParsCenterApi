using Data.IRepositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ParsCenterApi.Controllers.v2
{
    [ApiVersion("2")]
    public class CountryController : v1.CountryController
    {
        public CountryController(IRepository<Country> countryRepository) : base(countryRepository)
        {
        }
    }
}
