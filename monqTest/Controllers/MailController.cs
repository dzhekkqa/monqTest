using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using monqTest.Contracts.Requests;
using monqTest.Data;
using monqTest.Options;
using monqTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace monqTest.Controllers
{
    /// <summary>
    /// Контроллер, отвечающий за работу с сообщениями
    /// </summary>
    public class MailController: Controller
    {
        private IMailService _mailService;

        /// <summary>
        /// Конструктор контроллера для внедрения зависимости. В частности сервиса отправки сообщений
        /// </summary>
        /// <param name="mailService">Сервис отправки сообщений</param>
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Post запрос для отправки сообщений
        /// </summary>
        /// <param name="smr">Введенные пользователем данные (параметры сообщения)</param>
        /// <returns>Возвращает статус http запрос в зависимости от результата попытки отправить сообщение</returns>
        [HttpPost("api/mails/")]
        public async Task<IActionResult> PostRequest([FromBody]SendMailRequest smr)
        {
            Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (smr.mailRecipients == null)
                return BadRequest("Нет получателей!");
            int counter = 0;
            foreach (var recipient in smr.mailRecipients)
            {
                if (recipient.Trim() == null || !regex.IsMatch(recipient.Trim()))
                    counter++;
            }
            if (counter > 0)
                return BadRequest("Товары не могут быть пустыми!");
            var result = await _mailService.SendMail(smr);
            if (result != false)
                return Ok("Сообщение отправлено!");
            return BadRequest("Что-то пошло не так");
        }
        /// <summary>
        /// Get запрос для получения списка всех сообщений
        /// </summary>
        /// <returns>Возвращает статус http запрос в зависимости от результата попытки получить список сообщений</returns>
        [HttpGet("api/mails/")]
        public async Task<IActionResult> GetRequest()
        {
            var result = await _mailService.GetMails();
            if (result.Count < 1)
                return NotFound("Сообщений не найдено");
            return Ok(result);
        }
    }
}
