namespace DisputenPWA.Domain.Errors
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
