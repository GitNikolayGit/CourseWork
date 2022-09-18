using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Сoursework.Models
{
    public class Region : INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        // список улиц на участке
       // public List<Street> ListStreet { get; set; }

        // начальный номер дома на участке
        //public int MinHouse { get; set; }
        private int _minHouse;

        public int MinHouse
        {
            get { return _minHouse; }
            set {
                _minHouse = value; 
              //  OnPropertyChanged("MinHouse");
            }
        }


        // max номер дома на участке
        // public int MaxHouse { get; set; }
        private int _maxHouse;

        public int MaxHouse
        {
            get { return _maxHouse; }
            set {
                _maxHouse = value;
              //  OnPropertyChanged("MaxHouse");
            }
        }
        // строчное представление улиц
        private string? _streetStr;

        public string? StreetStr
        {
            get { return _streetStr; }
            set { _streetStr = value; }
        }



        // связь с почтальоном
        //public virtual List<Postman> Postmans { get; set; }
        private List<Postman> _postmans;

        public List<Postman> Postmans
        {
            get { return _postmans; }
            set {
                _postmans = value;
               // OnPropertyChanged("Postmans");
            }
        }


        // связь с улицами
        //public virtual List<Street> Streets { get; set; }
        private List<Street> _streets;

        public List<Street> Streets
        {
            get { return _streets; }
            set {
                _streets = value;
              //  OnPropertyChanged("Streets");
            }
        }


        // связь с заведующим
        // public virtual Manager Manager { get; set; }
        public Region()
        {
            Postmans = new List<Postman>();
            Streets = new List<Street>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
