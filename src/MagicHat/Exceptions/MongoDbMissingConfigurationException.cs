namespace MagicHat.Exceptions;

public sealed class MissingConfigurationSectionException : MagicHatException
{
    public MissingConfigurationSectionException(string module, string section)
        : base($"Configuration section '{section}' for module '{module}' is missing")
    {
    }
}
