using System.Text.Json;

namespace ValueTypes.Tests;

public class EmailTest
{
    [Fact]
    public void Test1()
    {
        var guid = Guid.NewGuid();

        var email1 = new Email("sl@dnmh.dk");
        var email2 = new Email("SL@dnmh.dk");
        var equal = email1 == email2;

        email1.Should().NotBeNull();
        equal.Should().BeTrue();
    }

    [Fact]
    public void Serialization()
    {
        var email = new Email("sl@dnmh.dk");

        var serialized = JsonSerializer.Serialize(email);
        var deserialized = JsonSerializer.Deserialize<Email>(serialized);

        email.Should().Be(deserialized);
    }
}
