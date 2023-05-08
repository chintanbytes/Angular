namespace Angular.Repositories;
public class Result<T>
{
    public bool Success { get; }
    public T Data { get; }
    public string ErrorMessage { get; }

    private Result(bool success, T data, string errorMessage)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> SetSuccess(T data)
    {
        return new Result<T>(true, data, default(string));
    }

    public static Result<T> SetFailure(string errorMessage)
    {
        return new Result<T>(false, default(T), errorMessage);
    }
}