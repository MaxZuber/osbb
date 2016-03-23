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
    }
}
