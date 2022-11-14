namespace Application.Common;

public abstract class Usecase<TRequest, TResponse>
{
    public abstract Task<TResponse> Execute(TRequest request);
}

public abstract class Usecase<T>
{
    public abstract Task<T> Execute();
}