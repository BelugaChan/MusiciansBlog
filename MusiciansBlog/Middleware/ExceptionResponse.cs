using System.Net;

namespace MusiciansBlog.API.Middleware
{
    public class ExceptionResponse
    {
        private HttpStatusCode statusCode;
        private string message;
        public ExceptionResponse(HttpStatusCode statusCode, string message)
        {
            this.statusCode = statusCode;
            this.message = message;
        }

        public HttpStatusCode StatusCode
        {
            get => statusCode;
        }

        public string Message
        {
            get => message;
        }
    }
}
