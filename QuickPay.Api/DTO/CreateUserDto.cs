using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace QuickPay.Api.DTO
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        public string Email { get;set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;


    }
}
