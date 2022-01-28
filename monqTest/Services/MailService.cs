using Microsoft.EntityFrameworkCore;
using monqTest.Contracts.Requests;
using monqTest.Data;
using monqTest.Models;
using monqTest.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace monqTest.Services
{
    /// <summary>
    /// Сервис отправки сообщений. Здесь находится логика записи в БД, отлов исключений и т.д.
    /// </summary>
    public class MailService: IMailService
    {
        /// <summary>
        /// Контекст данных или "таблица" писем
        /// </summary>
        private readonly DataContext _dataContext;

        /// <summary>
        /// Конструктор класса для внедрения зависимостей entity framework. Используется для манипуляции с данными в БД
        /// </summary>
        /// <param name="dataContext"></param>
        public MailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Вывод всех сообщений из БД
        /// </summary>
        /// <returns>Возвращает список сообщений из БД</returns>
        public async Task<List<MailModel>> GetMails()
        {
            return await _dataContext.Mails
                                     .ToListAsync();
        }

        /// <summary>
        /// Метод отправки сообщения пользователям и записи полученных результатов в БД
        /// </summary>
        /// <param name="smr">Полученные от пользователя данные сообщения</param>
        /// <returns>Возвращает булевое значение показывающее успешность/провал отправки сообщения</returns>
        public async Task<bool> SendMail(SendMailRequest smr)
        {
            var s = GetSettings();
            try
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = s.Host,
                    Port = s.Port,
                    EnableSsl = s.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = s.UserName,
                        Password = s.Password
                    }
                };
                var message = FormMessage(smr, s);
                smtp.Send(message);
                var dataToDb = MessageToDb(message, smr, true, String.Empty);
                await _dataContext.Mails.AddAsync(dataToDb);
                var created = await _dataContext.SaveChangesAsync();
                return created > 0;
            } catch (Exception ex)
            {
                var message = FormMessage(smr, s);
                var dataToDb = MessageToDb(message, smr, false, ex.Message);
                await _dataContext.Mails.AddAsync(dataToDb);
                var created = await _dataContext.SaveChangesAsync();
                return created > 0;
            }
        }

        /// <summary>
        /// Метод получения настроек отправки сообщений
        /// </summary>
        /// <returns>Возвращает объект типа SmtpOptions, содержащий настройки будущего объекта SmtpClient из конфигурационного файла</returns>
        public SmtpOptions GetSettings()
        {
            try
            {
                string[] directories = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
                int index = int.MaxValue;
                string fileName;
                for (int i = 0; i < directories.Length; i++)
                {
                    fileName = Path.GetFileName(Directory.GetFiles(System.IO.Directory.GetCurrentDirectory())[i]);
                    if (fileName.ToLower().Contains("smptSettings.json".ToLower()))
                    {
                        index = i;
                    }
                }
                string pathToFile = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory())[index];
                StreamReader r = new StreamReader(pathToFile);
                string jsonSettings = r.ReadToEnd();                
                SmtpOptions smtpOptions = JsonConvert.DeserializeObject<SmtpOptions>(jsonSettings);
                return smtpOptions;
            } catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Формирует сообщение для отправки
        /// </summary>
        /// <param name="smr">Параметры отправляемые пользователем</param>
        /// <param name="s">Текущие настройки отправки сообщения из доп. файла конфигурации smptSettings</param>
        /// <returns>Сформированное сообщение</returns>
        public MailMessage FormMessage(SendMailRequest smr, SmtpOptions s)
        {
            MailAddress fromEmail = new MailAddress(s.UserName, s.Sender);
            MailMessage message = new MailMessage()
            {
                From = fromEmail,
                Subject = smr.mailSubject,
                Body = smr.mailBody
            };
            foreach (var recepient in smr.mailRecipients)
            {
                message.To.Add(recepient.ToString());
            }
            return message;
        }

        /// <summary>
        /// Формирует данные для записи в БД. Удачный/неудачный результат
        /// </summary>
        /// <param name="message">Сформированное сообщение</param>
        /// <param name="smr">Полученные от пользователя данные по адресам, по которым должно прийти сообщение</param>
        /// <param name="success">Флаг, показывающий успех/не успех отправки сообщения</param>
        /// <param name="exception">Если отправка была неудачной, то в "причину" неудачи записываем ошибку</param>
        /// <returns>Возвращаем данные, которые будут записываться в БД</returns>
        public MailModel MessageToDb(MailMessage message, SendMailRequest smr, bool success, string exception)
        {
            MailModel sendedMessage = new MailModel()
            {
                Id = new Guid(),
                mailSubject = message.Subject,
                mailBody = message.Body,
                failedMessage = success ? String.Empty : exception,
                mailResult = success ? ResultEnum.OK : ResultEnum.Falied,
                mailRecipients = smr.mailRecipients,
                mailCreationDate = DateTime.Now
            };
            return sendedMessage;
        }
    }
}
