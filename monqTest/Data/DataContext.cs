using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using monqTest.Models;
using Newtonsoft.Json;

namespace monqTest.Data
{
    /// <summary>
    /// Класс контекста данных
    /// </summary>
    public class DataContext : IdentityDbContext
    {
        /// <summary>
        /// Конструктор класса контекста данных
        /// </summary>
        /// <param name="options">параметры</param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Таблица сообщений
        /// </summary>
        public DbSet<MailModel> Mails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailModel>()
                .Property(x => x.mailResult)
                .HasConversion<int>();

            modelBuilder.Entity<MailModel>().Property(p => p.mailRecipients)
                    .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<string[]>(v));

            base.OnModelCreating(modelBuilder);
        }
    }
}
