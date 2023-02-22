namespace CityInfo.API.Service
{
    public class CloudMailService : IMailService
    {
        public CloudMailService(IConfiguration config)
        {
            _mailTo = config["mailSettings:mailToAddress"];
            _mailFrom = config["mailSettings:mailFromAddress"];
        }
        private string _mailTo;
        private string _mailFrom;

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
