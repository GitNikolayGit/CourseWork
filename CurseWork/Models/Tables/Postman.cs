using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Сoursework.Models
{
    public class Postman : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set {
                _id = value;
               // OnPropertyChanged("Id");
            }
        }
        // фамилия
        private string _surname;

        public string Surname
        {
            get { return _surname; }
            set { 
                _surname = value; 
               // OnPropertyChanged("Surname");
            }
        }
        // имя
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set {
                _firstName = value;
                //OnPropertyChanged("FirstName");
            }
        }
        // отчество
        private string _patronymic;

        public string Patronymic
        {
            get { return _patronymic; }
            set { 
                _patronymic = value;
               // OnPropertyChanged("Patronymic");
            }
        }
        // строковое представление участков
        private string? _regionStr;

        public string? RegionStr
        {
            get { return _regionStr; }
            set { _regionStr = value; }
        }

        // участки
        private List<Region> _regions;
       
        public List<Region> Regions
        {
            get { return _regions; }
            set {
                _regions = value;
               // OnPropertyChanged("Regions");
            }
        }  
        // связь с участком
        //public virtual List<Region> Regions { get; set; }

        // связь с заведующим
       // public virtual Manager Manager { get; set; } 

        public Postman()
        {
            Regions = new List<Region>();
        }

        public override string ToString()
        {
            return $"{Id}";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
