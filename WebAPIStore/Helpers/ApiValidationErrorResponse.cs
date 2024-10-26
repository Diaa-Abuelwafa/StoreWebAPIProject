namespace WebAPIStore.Helpers
{
    public class ApiValidationErrorResponse : ApiErrorResponse
    {
        public List<string> Errors { get; set; }

        public ApiValidationErrorResponse(int Status, List<string> errors, string? Msg = null) : base(Status, Msg)
        {
            Errors = errors;
        }
    }
}
