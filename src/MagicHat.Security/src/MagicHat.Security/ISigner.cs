namespace MagicHat.Security;

public interface ISigner
{
    string GenerateSignature(object data, string secretKey);
    bool VerifySignature(object data, string secretKey, string signature);
}
