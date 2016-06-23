using System.ComponentModel.DataAnnotations;

namespace XCL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        public string Password { get; set; }
        public bool IsPersist { get; set; }
        public bool? InValidLogin { get; set; }
    }
}
