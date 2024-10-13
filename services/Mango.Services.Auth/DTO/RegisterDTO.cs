namespace mango.services.Auth.DTO{
public class RegisterDTO
{
 
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        
        public string RoleName {get;set;} = "USER";

    }
}