namespace API.Errors;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiErrorResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetMessageStatusCode(statusCode);
    }

    private string GetMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "An invalid request has been made",
            401 => "You are not authorized for this resource",
            404 => "Resource Not Found",
            500 => "Internal Error Server",
            _ => null
        };
    }
}