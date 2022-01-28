using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace monqTest
{
    /// <summary>
    /// Класс, определяющий точку входа в приложение
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Метод запуска приложения
        /// </summary>
        /// <param name="args">параметры</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Узел, отвечающий за запуск приложения и настройки приложения
        /// </summary>
        /// <param name="args">параметры</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
