using monqTest.Contracts.Requests;
using monqTest.Models;
using monqTest.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace monqTest.Services
{
    /// <summary>
    /// Интерфейс взаимодействия с сообщениями
    /// </summary>
    public interface IMailService
    {
        Task<List<MailModel>> GetMails();

        Task<bool> SendMail(SendMailRequest smr);

        SmtpOptions GetSettings();
    }
}
