namespace exercise1.models.config
{
    public class NotificationSettingsConfig
    {
        public string? senderEmail { get; set; }
        public int retrys { get; set; }
        public string[] administratorEmail { get; set; } = new string[0];
    }
}
