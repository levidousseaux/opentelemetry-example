using Application.Common;
using FluentValidation;

namespace Application.Usecases.RegisterUser;

public class RegisterUserRequest : Request
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }

    public override bool Validate()
    {
        var result = new RegisterUserRequestValidator().Validate(this);

        AddNotifications(result);

        return result.IsValid;
    }
}

internal class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}