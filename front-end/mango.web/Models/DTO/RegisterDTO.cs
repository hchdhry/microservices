namespace mango.web.models.DTO{
public class RegisterDTO
{
 
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        
        public string RoleName {get;set;} = "USER";

    }
}