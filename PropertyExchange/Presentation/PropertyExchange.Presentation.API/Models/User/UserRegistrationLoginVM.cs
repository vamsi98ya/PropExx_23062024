namespace PropertyExchange.Presentation.API.Models.User
{
    public class UserRegistrationLoginVM
    {
        public required string UserEmail { get; set; }
        public required string UserPhoneNumber { get; set; }
        public required string UserPassword { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public UserVM? UserDetails { get; set; }


        //public bool IsValid()
        //{
        //    return (string.IsNullOrEmpty(UserEmail) == false
        //        || string.IsNullOrEmpty(UserPhoneNumber) == false)
        //        && string.IsNullOrEmpty(UserPassword) == false;
        //}
    }

    public class UserChangePasswordVM
    {
        public required string UserEmail { get; set; }
        public required string UserPhoneNumber { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
