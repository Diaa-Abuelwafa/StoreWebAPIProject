namespace WebAPIStore.Helpers
{
    public class ApiExceptionErrorResponse : ApiErrorResponse
    {
        public string? Details { get; set; }
        public ApiExceptionErrorResponse(int Status, string? Msg = null, string? details = null) : base(Status, Msg)
        {
            Details = details;
        }
    }
}
