using Application.Common;

namespace Application.Usecases.RegisterUser;

public class RegisterUser : Usecase<RegisterUserRequest, RegisterUserResult>
{
    public override async Task<RegisterUserResult> Execute(RegisterUserRequest request)
    {
        if (!request.Validate())
        {
            return new RegisterUserResult { Errors = request.Errors };
        }


        return new RegisterUserResult
        {

        };
    }
}
