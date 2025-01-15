namespace json_web_api.Request
{
    public class AddJsonRequest
    {
        public string Text { get; set; } = string.Empty;

        public double Hour { get; set; } = 1;

        public object Settings { get; set; } = new();
    }
}
