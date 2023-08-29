namespace Ecom.API.Error
{
    public class BaseComonentResponse
    {
        public BaseComonentResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessage(statusCode);
        }

        private string DefaultMessage(int statuscode)
        => statuscode switch
        {
            400 => "Bad Request",
            401 => "Not Authorized",
            404 => "Not Found",
            500 => "Server Error",
            _ => null
        };

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
