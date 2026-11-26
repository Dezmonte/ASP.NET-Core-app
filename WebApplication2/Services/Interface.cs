namespace WebApplication2.Services
{
    public interface IMessageService
    {
        string GetMessage();
    }
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Привет из сервиса!";
        }
    }

    public class AnotherMessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Другой сервис";
        }
    }
}
