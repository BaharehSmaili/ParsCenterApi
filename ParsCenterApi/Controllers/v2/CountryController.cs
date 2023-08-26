using Data.IRepositories;
using Entities.Models.BasicInformation;
using Microsoft.AspNetCore.Mvc;

namespace ParsCenterApi.Controllers.v2
{
    [ApiVersion("2")]
    public class CountryController : v1.CountryController
    {
        //برای سوگر باید تمامی متد های موجود در کنترلر ورژن یک را اوررایت کنیم
        public CountryController(IRepository<Country> countryRepository) : base(countryRepository)
        {
        }
    }
}
