using System.Collections.Generic;
using BankMoneyLaunderer.Models;

namespace BankMoneyLaunderer.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        IEnumerable<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}