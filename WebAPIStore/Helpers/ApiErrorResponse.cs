namespace WebAPIStore.Helpers
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiErrorResponse(int Status, string? Msg = null)
        {
            StatusCode = Status;

            if(Msg is null)
            {
                Message = GetDefaultMessage(Status);
            }
            else
            {
                Message = Msg;
            }
        }

        private string? GetDefaultMessage(int Status)
        {
            string Msg = null;

            switch(Status)
            {
                case 404:
                    Msg = "Resource NotFound";
                    break;

                case 400:
                    Msg = "Invalid Request";
                    break;

                case 401:
                    Msg = "You Are Not Authorized";
                    break;

                case 500:
                    Msg = "Server Error";
                    break;
            }

            return Msg;
        }
    }
}
