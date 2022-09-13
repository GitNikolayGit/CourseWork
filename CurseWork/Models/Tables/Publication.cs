using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Сoursework.Models
{
    // издание
    public class Publication : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        // индекс издания
        private int _index;

        public int Index
        {
            get { return _index; }
            set {
                _index = value;
                OnPropertyChanged("Index");
            }
        }
        // название
        private string _title;

        public string Title
        {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        // тип издания
        private string _typePublic;

        public string TypePublic
        {
            get { return _typePublic; }
            set {
                _typePublic = value;
                OnPropertyChanged("TypePublic");
            }
        }
        // цена
        private int _price;

        public int Price
        {
            get { return _price; }
            set {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        // фото
        private string _foto;

        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }


        // связь с оператором
        //public virtual List<Operator> Operators { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
