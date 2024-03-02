using MagicHat.Security.Signing;

namespace MagicHat.SecurityTests;

internal record TestData(string Value1);

public class UnitTest1
{
    [Fact]
    public void ShouldVerifySuccessfullyGeneratedSignature()
    {
        // Arrange
        var signer = new Signer();
        var secretKey = "secret1";
        var data = new TestData("data1");
        
        // Act
        var signature = signer.GenerateSignature(data, secretKey);
        var verificationResult = signer.VerifySignature(data, secretKey, signature);
        
        // Assert
        Assert.True(verificationResult);
    }
}
