namespace Auth.Data.Interfaces.Services;

public interface IUserPasswordService
{

    string Encrypt(string password);
    string Decrypt(string decryptedPassword);

}