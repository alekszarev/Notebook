using Notebook.Service.Email;

namespace Notebook.Service.Contracts
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }


}
