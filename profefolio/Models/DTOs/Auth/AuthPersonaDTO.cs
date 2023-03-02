namespace profefolio.Models.DTOs.Auth;
// ReSharper disable once InconsistentNaming
public class AuthPersonaDTO : DataDTO
{
    public string? Username
    {
        get; 
        set;
    }

    public List<string> Roles
    {
        get;  
        private set;
    } = new List<string>();

    public DateTime Expires
    {
        get;
        set;
    }

    public string? Token
    {
        get;
        set;
    }
}

