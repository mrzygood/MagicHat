using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MagicHat.Security.Signing;

internal class Signer : ISigner
{
    public string GenerateSignature(object data, string secretKey)
    {
        var bodyAsJson = JsonSerializer.Serialize(data);
        return ComputeSha512Hash(bodyAsJson, secretKey);
    }

    public bool VerifySignature(object data, string secretKey, string signature)
    {
        var generatedSignature = GenerateSignature(data, secretKey);

        return generatedSignature.Equals(signature);
    }

    private static string ComputeSha512Hash(string rawData , string key)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] keyBytes = encoding.GetBytes(key);
        using HMACSHA512 hashAlg = new HMACSHA512(keyBytes);
        byte[] computedHash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < computedHash.Length; i++)
        {
            builder.Append(computedHash[i].ToString("x2"));
        }

        return builder.ToString();
    }
}
