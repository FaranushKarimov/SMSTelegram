using System.Net;

namespace SMSTelegram.Application.Responses;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; }

    public ApiResponse(T data)
    {
        Data = data;
        StatusCode = 200;
        Message = null;
    }
    public ApiResponse(T data,string message)
    {
        Data = data;
        StatusCode = 200;
        Message = null;
    }

    public ApiResponse(HttpStatusCode statusCode, string message)
    {
        Data = default;
        StatusCode = (int)statusCode;
        Message = message;
    }

    public ApiResponse()
    {
        throw new NotImplementedException();
    }
}