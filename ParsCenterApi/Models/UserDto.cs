using Entities;
using Entities.Models.BasicInformation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParsCenterApi.Models
{
    public class UserDto : IValidatableObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Family { get; set; }

        [Required]
        [StringLength(10)]
        public string NationalCode { get; set; }

        [Required]
        [StringLength(11)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NationalCode.Equals("0000000000", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمی تواند 0000000000 باشد", new[] { nameof(NationalCode) });
            if (NationalCode.Equals("1111111111", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمی تواند 1111111111 باشد", new[] { nameof(NationalCode) });
            if (Password.Equals("123456"))
                yield return new ValidationResult("رمز عبور نمی تواند 123456 باشد", new[] { nameof(Password) });
            if (!Email.Contains('@'))
                yield return new ValidationResult("ایمیل حتما باید @ داشته باشد", new[] { nameof(Email) });
        }
    }
}
