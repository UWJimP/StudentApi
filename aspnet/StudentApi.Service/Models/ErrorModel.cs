namespace StudentApi.Models
{
    public class ErrorModel
    {
        public string ErrorType { get; set; }
        
        public string Message { get; set; }

        public ErrorModel(){}

        public ErrorModel(string type, string message)
        {
            ErrorType = type;
            Message = message;
        }
    }
}
