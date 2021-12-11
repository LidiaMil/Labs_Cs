using System;
using System.Collections.Generic;
using System.Text;


namespace lab5
{
    [Serializable]
    public class Person {
        private string name;
        private string surname;
        private DateTime birthday;
        public Person (string _name, string _surname, DateTime _birthday) {
            name = _name;
            surname = _surname;
            birthday = _birthday;
        }
        public Person () : this ("Ivan", "Ivanov", new DateTime (2001, 10, 9)) {
            name = "Ivan";
            surname = "Ivanov";
            birthday = new DateTime (2001, 10, 9);
        }
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        public string Surname {
            get {
                return surname;
            }
            set {
                surname = value;
            }
        }
        public DateTime Birthday {
            get {
                return birthday;
            }
            set {
                birthday = new DateTime (value.Year, value.Month, value.Day);
            }
        }
        public int ChangeYear {
            get {
                return birthday.Year;
            }
            set {
                birthday = new DateTime (value, birthday.Month, birthday.Day);
            }
        }
        public override string ToString () {
            return name + " " + surname + " " + birthday.ToString ();
        }
        public virtual string ToShortString () {
            return name + " " + surname;
        }
        public override bool Equals (object obj) {
            // If the passed object is null
            if (obj == null) {
                return false;
            }
            if (!(obj is Person)) {
                return false;
            }
            return (this.name.Equals (((Person) obj).name)) &&
                (this.surname.Equals (((Person) obj).surname)) &&
                (this.birthday == ((Person) obj).birthday);
        }
        public override int GetHashCode () {
            return name.GetHashCode () ^ surname.GetHashCode () ^ birthday.GetHashCode ();
        }
        public static bool operator == (Person p1, Person p2) {
            // return (p1.name.Equals(p2.name))
            //     && (p1.surname.Equals(p2.surname))
            //     && (p1.birthday == p2.birthday);
            return p1.Equals (p2);
        }
        public static bool operator != (Person p1, Person p2) {
            // return !(p1.name.Equals(p2.name))
            //     || !(p1.surname.Equals(p2.surname))
            //     || !(p1.birthday == p2.birthday);
            return p1.Equals (p2);
        }
    }
}