using System.Net;

namespace EscolaWebSite.DTO
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }

    public class ErrorResponseDTO
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}