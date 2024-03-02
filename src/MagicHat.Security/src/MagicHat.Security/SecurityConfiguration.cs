namespace MagicHat.Security;

public sealed class SecurityConfiguration
{
    public SecurityConfiguration()
    {
    }
    
    public SecurityConfiguration(string secretKey)
    {
        SecretKey = secretKey;
    }

    public string SecretKey { get; set; }
}
