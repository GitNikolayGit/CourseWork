using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models;

namespace CurseWork.ContextDb.Configuration
{
    internal class DropCreateDb :
        DropCreateDatabaseIfModelChanges<MailContext>
        //DropCreateDatabaseAlways<MailContext>
    {
        protected override void Seed(MailContext db)
        {
            List<Street> streets = new List<Street>
            {
                new Street { Id = 0, Title = "Артема"},
                new Street { Id = 1, Title = "Ратникова"},
                new Street { Id = 2, Title = "Владычанского"},
                new Street { Id = 3, Title = "Шевченко"},
                new Street { Id = 4, Title = "Автоняна"},
                new Street { Id = 5, Title = "Набережная"}
            };
            List<Region> regions = new List<Region>
            {
                new Region { Id = 0, Streets = { streets[0], streets[1] }, MinHouse = 1, MaxHouse = 20 },
                new Region { Id = 1, Streets = { streets[0], streets[1] }, MinHouse = 21, MaxHouse = 40 },
                new Region { Id = 2, Streets = { streets[0], streets[1] }, MinHouse = 41, MaxHouse = 60 },

                new Region { Id = 3, Streets = { streets[2], streets[3] }, MinHouse = 1, MaxHouse = 20 },
                new Region { Id = 4, Streets = { streets[2], streets[3] }, MinHouse = 21, MaxHouse = 40 },
                new Region { Id = 5, Streets = { streets[2], streets[3] }, MinHouse = 41, MaxHouse = 60 },

                new Region { Id = 6, Streets = { streets[4], streets[5] }, MinHouse = 1, MaxHouse = 20 },
                new Region { Id = 7, Streets = { streets[4], streets[5] }, MinHouse = 21, MaxHouse = 40 },
                new Region { Id = 8, Streets = { streets[4], streets[5] }, MinHouse = 41, MaxHouse = 60 }
            };
            List<Postman> postmans = new List<Postman>
            {
                new Postman { Id = 0, Surname = "Ефремов", FirstName = "Сергей", Patronymic = "Иванович", Regions = { regions[0], regions[1] }},
                new Postman { Id = 1, Surname = "Деревянко", FirstName = "Валерий", Patronymic = "Сергеевич", Regions = { regions[2], regions[3] }},
                new Postman { Id = 2, Surname = "Гамершмидт", FirstName = "Евгений", Patronymic = "Федорович", Regions = { regions[4], regions[5] }},
                new Postman { Id = 3, Surname = "Андреев", FirstName = "Андрей", Patronymic = "Андреевич", Regions = { regions[6], regions[7] }},
                new Postman { Id = 4, Surname = "Демин", FirstName = "Сергей", Patronymic = "Анатольевич", Regions = { regions[8] }} 
            };
            List<Publication> publications = new List<Publication>
            {
                new Publication { Id = 0, Index = 15015, Title = "1000 советов дачнику", TypePublic = "газета", Price = 290  },
                new Publication { Id = 1, Index = 11440, Title = "STOP-газета", TypePublic = "газета", Price = 296 },
                new Publication { Id = 2, Index = 85493, Title = "Карусель времени", TypePublic = "газета", Price = 203 },
                new Publication { Id = 3, Index = 32128, Title = "АИФ. Здоровье", TypePublic = "газета", Price = 244 },
                new Publication { Id = 4, Index = 13015, Title = "3/9 Царство", TypePublic = "журнал", Price = 185 },
                new Publication { Id = 5, Index = 45364, Title = "За рулем", TypePublic = "журнал", Price = 394 },
                new Publication { Id = 6, Index = 11351, Title = "Педагогика", TypePublic = "журнал", Price = 1190 },
                new Publication { Id = 7, Index = 59979, Title = "Техника радиосвязи", TypePublic = "журнал", Price = 280 },
                new Publication { Id = 8, Index = 70610, Title = "На боевом посту", TypePublic = "журнал", Price = 251 },
                new Publication { Id = 9, Index = 43303, Title = "Здоровье", TypePublic = "журнал", Price = 524 },
                new Publication { Id = 10, Index = 11750, Title = "Аргументы и факты", TypePublic = "газета", Price = 427 }
            };
            List<Subscriber> subscribers = new List<Subscriber>
            {
                new Subscriber { Id = 0, Surname = "Проценко", FirstName = "Наталья", Patronymic = "Васильевна", Street = streets[0].Title, House = 55, Flat = 10, Index = publications[0].Index, StartDate = DateTime.Parse("10.03.2021"), MonthCount = 3 },
                new Subscriber { Id = 1, Surname = "Захарова", FirstName = "Валентина", Patronymic = "Федоровна", Street = streets[4].Title, House = 5, Flat = 30, Index = publications[1].Index, StartDate = DateTime.Parse("01.01.2021"), MonthCount = 12 },
                new Subscriber { Id = 2, Surname = "Нефедов", FirstName = "Артур", Patronymic = "Григорьевич", Street = streets[1].Title, House = 1, Flat = 1, Index = publications[2].Index, StartDate = DateTime.Parse("08.11.2021"), MonthCount = 2 },
                new Subscriber { Id = 0, Surname = "Курзин", FirstName = "Сергей", Patronymic = "Викторович", Street = streets[5].Title, House = 30, Flat = 15, Index = publications[3].Index, StartDate = DateTime.Parse("01.05.2021"), MonthCount = 9 },
                new Subscriber { Id = 0, Surname = "Капитонов", FirstName = "Илья", Patronymic = "Эдуардович", Street = streets[2].Title, House = 73, Flat = 33, Index = publications[10].Index, StartDate = DateTime.Parse("03.03.2021"), MonthCount = 5 },
                new Subscriber { Id = 0, Surname = "Лабузов", FirstName = "Алексей", Patronymic = "Васильевич", Street = streets[3].Title, House = 45, Flat = 50, Index = publications[8].Index, StartDate = DateTime.Parse("08.08.2021"), MonthCount = 1 },
                new Subscriber { Id = 0, Surname = "Корниенко", FirstName = "Наталья", Patronymic = "Пертравна", Street = streets[0].Title, House = 70, Flat = 8, Index = publications[4].Index, StartDate = DateTime.Parse("18.07.2021"), MonthCount = 3 },
                new Subscriber { Id = 0, Surname = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Street = streets[1].Title, House = 83, Flat = 21, Index = publications[5].Index, StartDate = DateTime.Parse("25.10.2021"), MonthCount = 2 },
                new Subscriber { Id = 0, Surname = "Проценко", FirstName = "Наталья", Patronymic = "Васильевна", Street = streets[0].Title, House = 55, Flat = 10, Index = publications[5].Index, StartDate = DateTime.Parse("15.05.2021"), MonthCount = 6 }
            };

            db.Streets.AddRange(streets);
            db.Regions.AddRange(regions);
            db.Postmans.AddRange(postmans);
            db.Publications.AddRange(publications);
            db.Subscribers.AddRange(subscribers);
            db.SaveChanges();

            // добавление процедур
            string connectString = "Server=(LocalDb)\\MSSQLLocalDB;initial catalog=CurseWork.ContextDb.MailContext;Trusted_Connection=True;";
            // уникальные подписчики
            string proc1 =
                @" 
create function DistinctSubscriber()
    returns table
    as
    return  
         select  distinct 
             Surname
             , FirstName
             , Patronymic   
         from
             Subscribers   
";
            // количество подписчиков
            string proc2 = @"
create function CountSubscriber()
    returns int
    as
    begin
        declare @n int
        select @n = (
        select
            count(*)
        from
            DistinctSubscriber()
        )
        return @n
    end
";
            // уникальные индексы изданий
            string proc3 = @"
create function DistinctPublication ()
    returns table
    as
    return
        select distinct
            [Index]
        from
            Subscribers
";
            // количество выписаных изданий
            string proc4 = @"
create function CountPublication()
    returns int
    as
    begin
        declare @n int
        select @n = (
            select
                count(*)
            from
                DistinctPublication()
            )
            return @n
    end
";
            using(SqlConnection connection = new(connectString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(proc1, connection);
                command.ExecuteNonQuery();
                command.CommandText = proc2;
                command.ExecuteNonQuery();
                command.CommandText = proc3;
                command.ExecuteNonQuery();
                command.CommandText = proc4;
                command.ExecuteNonQuery();
            }
        }
    }
}
