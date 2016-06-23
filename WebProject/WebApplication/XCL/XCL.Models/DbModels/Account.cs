using System.ComponentModel.DataAnnotations.Schema;

namespace XCL.Models.DbModels
{
    [Table("Account")]
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationToken { get; set; }
        public string Password { get; set; }
        public bool EmailIsConfirmed { get; set; }
        public bool PhoneNumberIsConfirmed { get; set; }
        public bool AccountIsVerified { get; set; }
        public bool SendEmailNotification { get; set; }
        public bool SendPhoneNotification { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? FlatId { get; set; }
        public Flat Flat { get; set; }
    }
}
