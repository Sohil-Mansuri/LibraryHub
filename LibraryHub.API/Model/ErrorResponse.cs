namespace LibraryHub.API.Model
{
    public class ErrorResponse(Dictionary<string, string> errors = null!, string message = null!, string errorCode = null!)
    {
        public Dictionary<string, string> Errors { get; set; } = errors;

        public string Message { get; set; } = message;

        public string ErrorCode { get; set; } = errorCode;

    }

}
