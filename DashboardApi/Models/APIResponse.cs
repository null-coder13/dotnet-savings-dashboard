using System.Net;

namespace DashboardApi;

public class APIResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; } = true;
    public List<string>? ErrorMessages { get; set; }
    public T? Result { get; set; }

}
