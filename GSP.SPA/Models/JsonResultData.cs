namespace GSP.SPA.Models
{
    public class JsonResultData
    {
        public bool IsSuccess { get; set; } = true;

        public object Data { get; set; }

        public string ErrorMessage { get; set; }

        public object ErrorData { get; set; }

        public static JsonResultData Success(object data = null)
        {
            return new JsonResultData
            {
                Data = data ?? new { }
            };
        }

        public static JsonResultData Error(string errorMessage, object errorData = null)
        {
            return new JsonResultData
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                ErrorData = errorData ?? new { }
            };
        }
    }
}
