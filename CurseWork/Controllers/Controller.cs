using CurseWork.ContextDb;
using CurseWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models;

namespace CurseWork.Controllers;

internal class Controller
{
    MailContext db;
     
    public  void ShowStreet(ObservableCollection<Street> list)
    {
        using  (db = new MailContext())
        {
            db.Streets.ToList().ForEach(s => list.Add(s));
        }
    }
    // вызов окна подписки
    public void SubscriberWindow(ObservableCollection<Subscriber> list)
    {
        SubscriberWindow window = new();
        window.Show();
        list.Clear();
        using (db = new MailContext())
        {
            db.Subscribers.ToList().ForEach(s => list.Add(s));
        }
        
    }
    // вызов окна издания
    public void PublicationWindow(ObservableCollection<Publication> list)
    {
        PublicationWindow window = new();
        window.Show();
        list.Clear();
        using (db = new MailContext())
        {
            db.Publications.ToList().ForEach(s => list.Add(s));
        }
        
    }
}

