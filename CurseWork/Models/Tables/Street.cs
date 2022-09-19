using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Сoursework.Models;

public class Street : INotifyPropertyChanged
{
    public int Id { get; set; }

    // название улицы
    private string _title;

    public string Title
    {
        get { return _title; }
        set {
            _title = value; 
          //  OnPropertyChanged("Title");
        }
    }


    // связь с участками
    // private List<Region> _regions;
    //
    // public List<Region> Regions
    // {
    //     get { return _regions; }
    //     set {
    //         _regions = value;
    //         OnPropertyChanged("Regions");
    //     }
    // }
    public List<Region> Regions { get; set; }


    public Street()
    {
        Regions = new List<Region>();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged(this, new PropertyChangedEventArgs(prop));
    } 
}

