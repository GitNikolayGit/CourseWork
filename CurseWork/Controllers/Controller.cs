using CurseWork.ContextDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models;

namespace CurseWork.Controllers
{
    internal class Controller
    {
        MailContext db;
         
        public  void ShowStreet(ObservableCollection<Street> list)
        {
            using (db = new MailContext())
            {
                db.Streets.ToList().ForEach(s => list.Add(s));
            }
        }
    }
}
