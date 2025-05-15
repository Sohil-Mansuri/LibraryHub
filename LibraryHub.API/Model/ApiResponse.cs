namespace LibraryHub.API.Model
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public ErrorResponse? Errors { get; set; }

        public static ApiResponse<T> Success(T data)
        {
            return new()
            {
                IsSuccess = true,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> Fail(ErrorResponse error)
        {
            return new()
            {
                IsSuccess = false,
                Data = default,
                Errors = error
            };
        }
    }
}
