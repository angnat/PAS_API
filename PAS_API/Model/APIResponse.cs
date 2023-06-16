using System.Net;

namespace PAS_API.Model
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessage { get; set; }
        public object Result { get; set; }
    }
}
