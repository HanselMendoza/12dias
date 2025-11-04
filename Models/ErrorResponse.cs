namespace rutaApiv1.Models
{
    public class ErrorResponse
    {
        public string TraceId { get; set; }
        public string Message { get; set; }
        public string? Detail { get; set; }

        public ErrorResponse(string traceId, string message, string? detail = null)
        {
            TraceId = traceId;
            Message = message;
            Detail = detail;
        }
    }
}