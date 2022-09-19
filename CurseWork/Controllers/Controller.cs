using CurseWork.ContextDb; 
using CurseWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Сoursework.Models;

namespace CurseWork.Controllers;

internal class Controller
{
    MailContext db;

    #region Заполнение списков

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

        if (!int.TryParse(window.tbHouse.Text, out int rez))
        {
            return "В поле номер дома ведено не число";
        }
        int index = -1;
        string surname = "";
        foreach (var item in listRegion)
        {
            if (item.StreetStr.Contains(window.cbStreet.Text) && item.MinHouse <= rez && item.MaxHouse >= rez){
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
    public string Square03( )
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
                info += "нет подписок";
        }    
        return info;
    }
    // 4.	Сколько почтальонов работает в почтовом отделении?
    public int Square04()
    {
        int count = 0;
        using (db = new MailContext())
        {
            count = db.Postmans.Count();
        }
        return count;
    }
    // 5.	На каком участке количество экземпляров подписных изданий максимально?
    public string Square05()
    {
        Dictionary<int, int> countPublic = new Dictionary<int, int>();
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
        return $"Максимальное количество подписных изданий {max}\n\n участок {region}";
    }
}

