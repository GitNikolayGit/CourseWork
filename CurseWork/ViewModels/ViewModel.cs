using CurseWork.ContextDb;
using CurseWork.Controllers; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models; 

namespace CurseWork.ViewModels;

internal class ViewModel : INotifyPropertyChanged
{
    Controller _controller = new Controller();

    private string _info;

    public string Info
    {
        get { return _info; }
        set {
            _info = value;
            OnPropertyChanged("Info");
        }
    }

    private string _street;

    public string Street
    {
        get { return _street; }
        set {
            _street = value;
            OnPropertyChanged("Street");
        }
    }
    // surname
    private string _surname;

    public string Surname
    {
        get { return _surname; }
        set {
            _surname = value;
            OnPropertyChanged("Surname");
        }
    }
    // name
    private string _name;

    public string Name
    {
        get { return _name; }
        set {
            _name = value;
            OnPropertyChanged("Name");
        }
    }
    // patronymic
    private string _patronymic;

    public string Patronymic
    {
        get { return _patronymic; }
        set {
            _patronymic = value;
            OnPropertyChanged("Patronymic");
        }
    } 


    public ObservableCollection<Postman> PostmanList { get; set; }
    public ObservableCollection<Publication> PublicList { get; set; }
    public ObservableCollection<Region> RegionList { get; set; }
    public ObservableCollection<Street> StreetsList { get; set; }
    public ObservableCollection<Subscriber> SubscribersList { get; set; }

    public ViewModel()
    {
        PostmanList = new ObservableCollection<Postman>();
        PublicList = new ObservableCollection<Publication>();
        RegionList = new ObservableCollection<Region>();
        StreetsList = new ObservableCollection<Street>();
        SubscribersList = new ObservableCollection<Subscriber>();
        _controller.FillStreet(StreetsList);
    }

    private void FillInfo(string file)
    {
        using (var sr = new StreamReader(file))
        {
            Info = sr.ReadToEnd();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    // открывает окно подписки
    private RelayCommand _subscriberWindowCommand;

    public RelayCommand SubscriberWindowCommand
    {
        get => _subscriberWindowCommand ??
            (_subscriberWindowCommand = new RelayCommand(obj =>
           {
               _controller.SubscriberWindow(SubscribersList);
           }));
    }
    // открывает окно издания
    private RelayCommand _publicationWindowCommand;

    public RelayCommand PublicationWindowCommand
    {
        get => _publicationWindowCommand ??
            (_publicationWindowCommand = new RelayCommand(obj =>
            {
                _controller.PublicationWindow(PublicList);
            }));
    }
    // открывает окно почтальоны
    private RelayCommand _postmanWindowCommand;

    public RelayCommand PostmanWindowCommand
    {
        get => _postmanWindowCommand ??
            (
               _postmanWindowCommand = new RelayCommand(obj =>
               {
                   _controller.PostmanWindow(PostmanList);
               }));
    }
    // открывает окно регионы
    private RelayCommand _regionWindowCommand;

    public RelayCommand RegionWindowCommand
    {
        get => _regionWindowCommand ??
            (
                _regionWindowCommand = new RelayCommand(obj =>
                {
                    _controller.RegionWindow(RegionList);
                }));
    }
    // выводит информацию о запросах
    private RelayCommand _infoCommand;

    public RelayCommand InfoCommand
    {
        get => _infoCommand ??
            (
                _infoCommand = new RelayCommand(obj =>
                {
                    FillInfo("text.txt");
                }));
    }
    // выводит информацию о отчетах
    private RelayCommand _info2Command;

    public RelayCommand Info2Command
    {
        get => _info2Command ??
            (
                _info2Command = new RelayCommand(obj =>
                {
                    FillInfo("text2.txt");
                }));
    }


    #region запросы

    // запрос 1
    private RelayCommand _square01Command;

    public RelayCommand Square01Command
    {
        get => _square01Command ??
            (
                _square01Command = new RelayCommand(obj =>
               {
                   Info = _controller.Square01();
               }));
    }
    // запрос 2
    private RelayCommand _square02Command;

    public RelayCommand Square02Command
    {
        get => _square02Command ??
            (
                _square02Command = new RelayCommand(obj =>
                {
                    Info = _controller.Square02(PostmanList, RegionList);
                }));
    }
    // запрос 3
    private RelayCommand _square03Command;

    public RelayCommand Square03Command
    {
        get => _square03Command ??
            (
                _square03Command = new RelayCommand(obj =>
                {
                    Info = _controller.Square03(); 
                }));
    }
    // запрос 4
    private RelayCommand _square04Command;

    public RelayCommand Square04Command
    {
        get => _square04Command ??
            (
                _square04Command = new RelayCommand(obj =>
                {
                    Info = $"В почтовом отделении работает {_controller.Square04()} почтальонов";
                }));
    }
    // запрос 5
    private RelayCommand _square05Command;

    public RelayCommand Square05Command
    {
        get => _square05Command ??
            (
                _square05Command = new RelayCommand(obj =>
                {
                    Info = _controller.Square05();
                }));
    }
    // запрос 6
    private RelayCommand _square06Command;

    public RelayCommand Square06Command
    {
        get => _square06Command ??
            (
                _square06Command = new RelayCommand(obj =>
                {
                    Info = _controller.Square06();
                }));
    }
    #endregion
    // справка
    private RelayCommand _referenceCommand;

    public RelayCommand ReferenceCommand
    {
        get => _referenceCommand ??
            (
                _referenceCommand = new RelayCommand(obj =>
               {
                   Info = _controller.Reference();
               }));
    }
    // отчет 2
    private RelayCommand _reportCommand;

    public RelayCommand ReportCommand
    {
        get => _reportCommand ??
            (
                _reportCommand = new RelayCommand(obj =>
                {
                    Info = _controller.Report2();
                }));
    }
    // отчет 3
    private RelayCommand _report3Command;

    public RelayCommand Report3Command
    {
        get => _report3Command ??
            (
                _report3Command = new RelayCommand(obj =>
                {
                    Info = _controller.Report3();
                }));
    }

}

