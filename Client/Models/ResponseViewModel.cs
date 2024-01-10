namespace Client.Models
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessages { get; set; } = "";

        public List<string> ErrorMessages { get; set; }
    }
}
