namespace MagicHat.Security;

public sealed class SecurityMissingConfigurationException : MagicHatException
{
    public SecurityMissingConfigurationException() : base("Security configuration is missing")
    {
    }
}
