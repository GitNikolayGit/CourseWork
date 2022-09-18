using CurseWork.ContextDb; 
using CurseWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models;

namespace CurseWork.Controllers;

internal class Controller
{
    MailContext db;

    #region Заполнение списков

    // заполнение списка почтальоны
    public void FillPostman(ObservableCollection<Postman> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            var l = db.Postmans.Include(c => c.Regions).ToList();
            foreach (var c in l)
            {
                Postman p = new();
                p.Id = c.Id;
                p.Surname = c.Surname;
                p.FirstName = c.FirstName;
                p.Patronymic = c.Patronymic;
                foreach (var d in c.Regions)
                    p.RegionStr += $"{d.Id}, ";
                list.Add(p);
            }
        }
    }
    // заполнение списка участок
    public void FillRegion(ObservableCollection<Region> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            var l = db.Regions.Include(c => c.Streets);
            foreach (var c in l)
            {
                Region p = new();
                p.Id = c.Id;
                p.MinHouse = c.MinHouse;
                p.MaxHouse = c.MaxHouse;
                foreach (var d in c.Streets)
                {
                    p.StreetStr += $"{d.Title}, ";
                }
                list.Add(p);
            }
        }
    }
    // заполнение списка подписки
    public void FillSubcriber(ObservableCollection<Subscriber> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            db.Subscribers.ToList().ForEach(s => list.Add(s));
        }
    }
    // заполнение списка издания
    public void FillPublication(ObservableCollection<Publication> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            db.Publications.ToList().ForEach(s => list.Add(s));
        }
    }

    #endregion

    #region вызов окон

    // вызов окна подписки
    public void SubscriberWindow(ObservableCollection<Subscriber> list)
    {
        SubscriberWindow window = new();
        window.Show();
        if (!list.Any())
            FillSubcriber(list);
    }
    // вызов окна издания
    public void PublicationWindow(ObservableCollection<Publication> list)
    {
        PublicationWindow window = new();
        window.Show();
        if (!list.Any())
            FillPublication(list);
    }
    
    // вызов окна почтальоны
    public void PostmanWindow(ObservableCollection<Postman> list)
    {
        PostmanWindow window = new();
        window.Show();
        if (!list.Any())
            FillPostman(list);
    }
    
    // вызов окна участки
    public void RegionWindow(ObservableCollection<Region> list)
    {
        RegionWindow window = new();
        window.Show();
        if (!list.Any())
            FillRegion(list);
    }

    #endregion

    // 1.	Определить наименование и количество экземпляров
    // всех изданий, получаемых отделением связи
    public string Square01(ObservableCollection<Publication> list)
    {
        string info = "Наименования всех изданий\n\n";
        //using (db = new MailContext())
        //{
        //    db.Publications.ToList().ForEach(c => info += $"{c.Title}\n");
        //    int n = db.Publications.Count();
        //    info += $"\nколичество экземпляров всех изданий {n}";
        //}
        if (!list.Any())
            FillPublication(list);
        foreach (var item in list)
        {
            info += $"{item.Title}\n";
        }
       // int n = db.Publications.Count();
        return info += $"\nколичество экземпляров всех изданий {list.Count}";
       // return info;
    }
    // 2.	По заданному адресу определить фамилию почтальона, обслуживающего подписчика
   // public string Square02(string street, int house, int flat = 0)
  //  {
   //     StringBuilder str;
  //      int index = -1;
  //      
   // }

}

