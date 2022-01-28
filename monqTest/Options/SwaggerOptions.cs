using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monqTest.Options
{
    /// <summary>
    /// Настройки сваггера для удобства тестирования
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// Пользовательский путь до swagger.json
        /// </summary>
        public string JsonRoute { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Адрес расположения интерфейса сваггера
        /// </summary>
        public string UiEndpoint { get; set; }
    }
}
