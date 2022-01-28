using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monqTest.Options
{
    /// <summary>
    /// Модель настроек для SmtpClient
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// Хост для SmtpClient
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Порт для SmtpClient
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Подключение ssl для SmtpClient
        /// </summary>
        public bool EnableSsl { get; set; }
        /// <summary>
        /// Логин от почты отправителя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Пароль от почты отправителя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Отображаемое имя отправителя
        /// </summary>
        public string Sender { get; set; }
    }
}
