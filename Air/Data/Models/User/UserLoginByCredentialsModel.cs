using Auth.Data.Dtos.User;

namespace Air.Data.Models.User;

public class UserLoginByCredentialsModel
{
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }

    public UserLoginByCredentialsDto ToDto()
    {
        return new UserLoginByCredentialsDto()
        {
            Username = Username,
            Password = Password,
        };
    }
    
}