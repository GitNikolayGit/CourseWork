using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Сoursework.Models;

namespace CurseWork.ContextDb
{
    public class MailContext : DbContext
    {
        // Контекст настроен для использования строки подключения "MailContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "CurseWork.ContextDb.MailContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "MailContext" 
        // в файле конфигурации приложения.
        public MailContext()
            : base("name=MailContext")
        {
            Database.SetInitializer<MailContext>(new Configuration.DropCreateDb());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Postman> Postmans { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
       // public DbSet<Manager> Managers { get; set; }
        public DbSet<Publication> Publications { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}