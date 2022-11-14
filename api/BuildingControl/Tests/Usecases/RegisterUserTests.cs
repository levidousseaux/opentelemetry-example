using Application.Usecases.RegisterUser;

namespace Tests.Usecases;

public class RegisterUserTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task RegisterUser_InvalidName_ReturnsError(string? name)
    {
        var request = new RegisterUserRequest { Name = name };

        var result = await new RegisterUser().Execute(request);

        Assert.True(result.Errors.Any());
    }
}
