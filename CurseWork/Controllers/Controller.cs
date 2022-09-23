using CurseWork.ContextDb; 
using CurseWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Сoursework.Models;

namespace CurseWork.Controllers;

internal class Controller
{
    MailContext db;

    #region функциии

    // заполнение спмска улицы
    public void FillStreet(ObservableCollection<Street> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            db.Streets.ToList().ForEach(s => list.Add(s));
        }
    }
    // заполнение списка почтальоны
    private void FillPostman(ObservableCollection<Postman> list)
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

    // 

    // заполнение списка участок
    private void FillRegion(ObservableCollection<Region> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            // var l = db.Database.SqlQuery<Region>("all_Region");
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
    private void FillSubcriber(ObservableCollection<Subscriber> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            db.Subscribers.ToList().ForEach(s => list.Add(s));
        }
    }
    // заполнение списка издания
    private void FillPublication(ObservableCollection<Publication> list)
    {
        list.Clear();
        using (db = new MailContext())
        {
            db.Publications.ToList().ForEach(s => list.Add(s));
        }
    }

    //  количество выписаных изданий
    private int CountDictinctPublication()
    {
        using (db = new MailContext())
        {
            return db.Publications.Select(e => e.Index).Distinct().Count();
        }
    }
    // количество газет
    public int CountNewspaper()
    {
        using (db = new MailContext())
        {
            return db.Publications.Where(e => e.TypePublic == "газета").Count();
        }
    }
    // количество журналов
    public int CountJournal()
    {
        using (db = new MailContext())
        {
            return db.Publications.Where(e => e.TypePublic == "журнал").Count();
        }
    }
    // количество подписчиков
    private int CountSubscriber()
    {
        using (db = new MailContext())
        {
            var pub = db.Subscribers;
            return (from e in pub
                    select new
                    {
                        e.Surname
                        ,
                        e.FirstName
                        ,
                        e.Patronymic
                    }).Distinct().Count();
        }
    }
    // количество почтальонов
    private int CountPostman()
    {
        using (db = new MailContext())
        {
            return db.Postmans.Count();
        }
    }
    // количество участков
    private int CountRegion()
    {
        using (db = new MailContext())
        {
            return db.Regions.Count();
        }
    }
    // средний срок подписки по изданию 
    private string MidleSubscriberPublic(int index)
    {
        string info = "";
        using (db = new MailContext())
        {
            string title = db.Publications.ToList().Find(e => e.Index == index).Title;
            var subscriber = db.Subscribers.ToList();
            int n = subscriber.FindAll(e => e.Index == index).Count();
            int n2 = subscriber.Where(e => e.Index == index).Sum(e => e.MonthCount);
            if (n > 0 && n2 > 0)
                info += $"Издание {title}\nсредний срок подписки {n2 / n}\n\n";
            else
                info += $"{title}\nнет подписок\n\n";
            return info;
        }
    }
    // название издания по индексу
    private string IndexTitle(int index)
    {
        using (db = new MailContext())
        {
            return db.Publications.ToList().Find(e => e.Index == index).Title;
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

    #region запросы

    // 1.	Определить наименование и количество экземпляров
    // всех изданий, получаемых отделением связи
    public string Square01()
    {
        string info = "Наименования всех изданий\n\n";
        int count = 0;
        using (db = new MailContext())
        {
            var title = db.Publications;
            count = title.Count();
            foreach (var item in title) info += $"{item.Title}\n";

        }
        return info += $"\nколичество экземпляров всех изданий {count}";
    }
    // 2.	По заданному адресу определить фамилию почтальона, обслуживающего подписчика
    // вызов окна поиск по адресу
    public string Square02(ObservableCollection<Postman> listPostman, ObservableCollection<Region> listRegion)
    {
        if (!listPostman.Any()) FillPostman(listPostman);
        if (!listRegion.Any()) FillRegion(listRegion);

        AddressWindow window = new();
        window.ShowDialog();

        int rez = int.Parse(window.tbHouse.Text);
        int index = -1;
        string surname = "";
        foreach (var item in listRegion)
        {
            if (item.StreetStr.Contains(window.cbStreet.Text) && item.MinHouse <= rez && item.MaxHouse >= rez)
            {
                index = listRegion.First(e => e.StreetStr.Contains(window.cbStreet.Text) && e.MinHouse <= rez && e.MaxHouse >= rez).Id;
            }
        }
        if (index == -1) return $"Улица {window.cbStreet.Text} дом {rez}\nнет такого адреса";
        foreach (var item in listPostman)
        {
            foreach (var item2 in item.RegionStr.Split(','))
            {
                if (item2.Contains($"{index}"))
                    surname = item.Surname;
            }
        }
        return $"Адрес:\nУлица {window.cbStreet.Text} дом {rez}\nучасток {index}\n\nобслуживает почтальон\n{surname}";
    }
    // 3.	Какие газеты выписывает гражданин с указанной фамилией, именем, отчеством?
    public string Square03()
    {
        string surname, name, patronymic;

        PiopleWindow window = new PiopleWindow();
        window.ShowDialog();
        surname = window.tbSurname.Text;
        name = window.tbName.Text;
        patronymic = window.tbPatronymic.Text;

        string info = $"{surname} {name} {patronymic}\n\n";

        using (db = new MailContext())
        {
            var sub = db.Subscribers.Where(e => e.Surname == surname && e.FirstName == name && e.Patronymic == patronymic).Select(e => e.Index);
            var pub = db.Publications.Where(e => sub.Contains(e.Index)).ToList();

            if (pub.Count > 0)
            {
                info += "получает издание\n\n";
                pub.ForEach(e => info += $"{e.Title} \n");
            }
            else
                info += "нет подписок\n\n";
        }
        return info;
    }
    // 4.	Сколько почтальонов работает в почтовом отделении?
    public int Square04()
    {
        using (db = new MailContext())
        {
            return db.Postmans.Count();
        }
    }
    // 5.	На каком участке количество экземпляров подписных изданий максимально?
    public string Square05()
    {
        Dictionary<int, int> countPublic = new();
        using (db = new MailContext())
        {
            foreach (var item in db.Regions.Include(e => e.Streets))
            {
                int n = 0;
                foreach (var item2 in db.Subscribers)
                {
                    foreach (var street in item.Streets)
                    {
                        if (street.Title == item2.Street && item2.House > item.MinHouse && item2.House < item.MaxHouse)
                        {
                            countPublic[item.Id] = ++n;
                        }
                    }
                }
            }
        }
        int max = countPublic.Values.Max();
        int region = countPublic.First(e => e.Value == max).Key;
        return $"Максимальное количество подписных изданий {max}\n\nна участке {region}";
    }
    // 6.	Каков средний срок подписки по каждому изданию 
    public string Square06()
    {
        string info = "Средний срок по подпискам\n\n";
        using (db = new MailContext())
        {
            db.Publications.ToList().ForEach(e => info += MidleSubscriberPublic(e.Index));
        }
        return info;
    }

    #endregion

    // справка о количестве подписчиков, количестве газет и количестве журналов
    public string Reference()
    {
        string info = "Количество подписчиков: ";
        using (db = new MailContext())
        {
            info += $"{CountSubscriber()}\n";
            info += $"Количество газет {CountNewspaper()}\n";
            info += $"Количество журналов {CountJournal()}\n";
        }

        return info;
    }
    // Для каждого участка указывается фамилия и инициалы почтальона, обслуживающего участок,
    // и перечень доставляемых изданий (индекс и название издания, адрес доставки, срок подписки).
    // По каждому изданию указывается средний срок подписки и количество экземпляров,
    // а по участку – количество различных подписных изданий. 
    public string Report2()
    {
        int count = 0;
        double period = 0;
        string info = "";
        int countPublic = 0;
        using (db = new MailContext())
        {
            var postman = db.Postmans.Include(e => e.Regions).ToList();
            var subscrebers = db.Subscribers;
            foreach (var region in db.Regions.Include(e => e.Streets))
            {
                info += $"Участок {region.Id + "\n\n"}";

                //var postman = db.Postmans.Include(e => e.Regions).ToList();//.ToList().Find(e => e.Regions.Contains(region));
                foreach (var pos in postman)
                {
                    if (pos.Regions.Contains(region))
                        info += $"Почтальон\n{pos.Surname} {pos.FirstName} {pos.Patronymic}\n\n";
                }
                List<string> streetList = new();
                region.Streets.ForEach(e => streetList.Add(e.Title));

                foreach (var subscriber in subscrebers)
                {
                    if (streetList.Contains(subscriber.Street)
                        && region.MinHouse < subscriber.House
                        && region.MaxHouse > subscriber.House)
                    {
                        countPublic++;
                        info += $"Индекс: {subscriber.Index}\nназвание {IndexTitle(subscriber.Index)}\n" +
                            $"адрес: {subscriber.Street} {subscriber.House} {subscriber.Flat}\n" +
                            $"срок подписки: {subscriber.MonthCount} месяцев\n\n";
                        count++;
                        period += subscriber.MonthCount;
                    }
                }
                double midlle = count > 0 ? period / count : 0;
                info += $"средний срок подписок: {midlle}\n";
                info += $"количество изданий на участе: {countPublic}\n---------------------------------------\n\n";
                countPublic = 0;
                count = 0;
                period = 0;
            }
        }
        return info;
    }
    // сколько почтальонов работает в почтовом отделении,
    // сколько всего участков оно обслуживает,
    // сколько различных изданий доставляет подписчикам
    public string Report3() 
        => $"Почтальонов: {CountPostman()}\nУчастков: {CountRegion()}\nДоставляемых изданий: {CountDictinctPublication()}"; 
}