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



    public ObservableCollection<Postman> PostmanList { get; set; }
    public ObservableCollection<Publication> PublicList { get; set; }
    public ObservableCollection<Region> RegionList { get; set; }
   // public ObservableCollection<Street> StreetsList { get; set; }
    public ObservableCollection<Subscriber> SubscribersList { get; set; }

    public ViewModel()
    {
        PostmanList = new ObservableCollection<Postman>();
        PublicList = new ObservableCollection<Publication>();
        RegionList = new ObservableCollection<Region>();
    //    StreetsList = new ObservableCollection<Street>();
        SubscribersList = new ObservableCollection<Subscriber>();
    }

    private void FillInfo()
    {
        using (var sr = new StreamReader("text.txt"))
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

    // команды
   // private RelayCommand _showStreetCommand;
   //
   // public RelayCommand ShowStreetCommand
   // {
   //     get => _showStreetCommand ??
   //         (_showStreetCommand = new RelayCommand(obj =>
   //         {
   //             _controller.ShowStreet(StreetsList);
   //         }));
   // }
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
    // выводит информацию
    private RelayCommand _InfoCommand;

    public RelayCommand InfoCommand
    {
        get => _InfoCommand ??
            (
                _InfoCommand = new RelayCommand(obj =>
                {
                    FillInfo();
                }));
    }
    // запрос 1
    private RelayCommand _square01Command;

    public RelayCommand Square01Command
    {
        get => _square01Command ??
            (
                _square01Command = new RelayCommand(obj =>
               {
                   Info = _controller.Square01(PublicList);
               }));
    }




}

