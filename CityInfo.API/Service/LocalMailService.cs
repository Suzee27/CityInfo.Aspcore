namespace CityInfo.API.Service
{
    public class LocalMailService : IMailService
    {
        public LocalMailService(IConfiguration config)
        {
            _mailTo = config["mailSettings:mailToAddress"];
            _mailFrom = config["mailSettings:mailFromAddress"];
        }
        private readonly string _mailTo;
        private readonly string _mailFrom;

        public void Send(string subject, string message)
        {
            Console.WriteLine($"mail from {_mailFrom} to {_mailTo}, " +
                $"with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
