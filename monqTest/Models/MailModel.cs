using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace monqTest.Models
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public class MailModel
    {
        /// <summary>
        /// Первичный ключ сообщения
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string mailSubject { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string mailBody { get; set; }
        /// <summary>
        /// Получатели сообщениия
        /// </summary>
        public string[] mailRecipients { get; set; }
        /// <summary>
        /// Дата создания сообщения
        /// </summary>
        public DateTime mailCreationDate { get; set; }
        /// <summary>
        /// Статус сообщения: OK - успешно отправлено, Failed - не отправлено
        /// </summary>
        public ResultEnum mailResult { get; set; }
        /// <summary>
        /// Ошибка отправки сообщения
        /// </summary>
        public string failedMessage { get; set;  }
    }
}
