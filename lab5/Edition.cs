using System;
using System.ComponentModel;


namespace lab5
{
    [Serializable]
    public class Edition: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected string name;
        protected DateTime date;
        protected int circ;
        public Edition(string _name, DateTime _date, int _circ)
        {
            name = _name;
            date = _date;
            circ = _circ;
        }
        public Edition()
        {
            name = "NAME";
            date = new DateTime(2020,10,1);
            circ = 1;
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = new DateTime (value.Year, value.Month, value.Day);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date"));
            }
        }
        public int Circ
        {
            get
            {
                return circ;
            }
            set
            {
                if (value < 0)
                {
                    ArgumentException ex = new ArgumentException("Введите неотрицательное целое число");
                    throw ex;
                }
                else
                    circ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Circ"));
            }
        }
        public virtual object DeepCopy()
        {
            return new Edition(this.name, this.date, this.circ);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            if (!(obj is Edition)) {
                return false;
            }
            return (this.circ == ((Edition) obj).circ) &&
                (this.name.Equals (((Edition) obj).name)) &&
                (this.date == ((Edition) obj).date);
        }
        public override int GetHashCode () {
            return name.GetHashCode () ^ date.GetHashCode () ^ circ.GetHashCode ();
        }
        public static bool operator == (Edition e1, Edition e2) {
            return e1.Equals (e2);
        }
        public static bool operator != (Edition e1, Edition e2) {
            return !e1.Equals (e2);
        }
        public override string ToString()
        {
            return name + " " + date.ToShortDateString() + " " + circ.ToString();
        }
    }
}