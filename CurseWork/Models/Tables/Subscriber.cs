using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сoursework.Models
{
    // подписки
    public class Subscriber
    {
        public int Id { get; set; }

        // подписчик
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        // адрес
        public string Street { get; set; }

        public int House { get; set; }

        public int? Flat { get; set; }

        // индекс издания
        public int Index { get; set; }

        // дата подписки
        public DateTime StartDate { get; set; }

        // длительность подписки
        public int MonthCount { get; set; } 
    }
}