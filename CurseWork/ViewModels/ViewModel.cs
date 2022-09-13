using CurseWork.ContextDb;
using CurseWork.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Сoursework.Models; 

namespace CurseWork.ViewModels;

internal class ViewModel
{
    Controller _controller = new Controller();


    public ObservableCollection<Postman> PostmanList { get; set; }
    public ObservableCollection<Publication> Publications { get; set; }
    public ObservableCollection<Region> RegionList { get; set; }
    public ObservableCollection<Street> StreetsList { get; set; }
    public ObservableCollection<Subscriber> SubscribersList { get; set; }

    public ViewModel()
    {
        PostmanList = new ObservableCollection<Postman>();
        Publications = new ObservableCollection<Publication>();
        RegionList = new ObservableCollection<Region>();
        StreetsList = new ObservableCollection<Street>();
        SubscribersList = new ObservableCollection<Subscriber>();
    }

    private RelayCommand _showStreetCommand;

    public RelayCommand ShowStreetCommand
    {
        get => _showStreetCommand ??
            (_showStreetCommand = new RelayCommand(obj =>
            {
                _controller.ShowStreet(StreetsList);
            }));
        }

}

