namespace WebApiCitas.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationRequest
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}