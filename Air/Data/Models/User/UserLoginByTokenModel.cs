using System.ComponentModel.DataAnnotations;
using Auth.Data.Dtos.User;

namespace Air.Data.Models.User;

public class UserLoginByTokenModel
{
    
    [JsonPropertyName("token")]
    [Required]
    public string Token { get; set; }

    public UserLoginByTokenDto ToDto()
    {
        return new UserLoginByTokenDto()
        {
            Token = Token,
        };
    }
    
}