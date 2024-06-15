namespace User.Infastructure.Abstractions;

public interface IPasswordHasher
{
    
    public string Generate(string password);

    public bool Verify(string password, string hash);


}