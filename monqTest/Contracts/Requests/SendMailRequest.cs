using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace monqTest.Contracts.Requests
{
    /// <summary>
    /// Модель, в которую пользователь будет "помещать" данные
    /// </summary>
    public class SendMailRequest
    {
        /// <summary>
        /// Тема сообщения
        /// </summary>
        [Required]
        public string mailSubject { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        [Required]
        public string mailBody { get; set; }
        /// <summary>
        /// Получатели сообщения
        /// </summary>
        [Required]
        public string[] mailRecipients { get; set; }
    }
}
