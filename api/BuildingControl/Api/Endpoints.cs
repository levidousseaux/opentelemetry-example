using Application.Common;
using Application.Usecases.RegisterUser;

namespace Api;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/users").MapUsers().WithOpenApi();
    }

    public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
    {
        group.MapPost("/", async Task<IResult> (RegisterUserRequest request, RegisterUser usecase) => BuildResult(await usecase.Execute(request)));

        return group;
    }

    public static IResult BuildResult<TResult>(TResult result) where TResult : Result
    {
        if (result.Errors != null && result.Errors.Any())
        {
            return Results.BadRequest(result.Errors);
        }

        if (result.Status != null && result.Status > 0)
        {
            return Results.Json(data: result, statusCode: result.Status);
        }

        return Results.Ok(result);
    }
}
