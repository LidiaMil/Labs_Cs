using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.IO;

namespace lab5
{
    [Serializable]
    public class Magazine : Edition, IRateAndCopy, IEnumerable, INotifyPropertyChanged
    {
        private Frequency freq;
        private List<Article> articleList;
        private List<Person> editors;
        public Magazine (string _name, Frequency _freq, DateTime _date, int _circ) {
            name = _name;
            freq = _freq;
            date = _date;
            circ = _circ;
            articleList = new List<Article>();
            editors = new List<Person>();
        }
        public Magazine () : this("TITLE", Frequency.Weekly, new DateTime(2020, 10, 10), 1){}
        public Frequency Freq {
            get {
                return freq;
            }
            set {
                freq = value;
            }
        }
        public List<Article> ArticleList {
            get {
                return articleList;
            }
            set {
                articleList = value;
            }
        }
        public List<Person> Editors
        {
            get{
                return editors;
            }
            set{
                editors = value;
            }
        }
        public double AvRating {
            get {
                double sum = 0;
                foreach (var art in articleList) {
                    sum += art.rating;
                }
                return sum / articleList.Count;
            }
        }
        public bool this [Frequency f] {
            get {
                return freq == f;
            }
        }
        public void AddArticles (List<Article> _articleList) {
            // int a = articleList.Length;
            // Array.Resize(ref articleList, articleList.Length + _articleList.Length);
            // for (int i = a; i < articleList.Length; i++)
            // {
            //     articleList[i]=_articleList[i-a];
            // }
            articleList = articleList.Concat (_articleList).ToList ();
        }
        public void AddEditors(List<Person> _editors)
        {
            editors = editors.Concat(_editors).ToList();
        }
        public override string ToString () {
            string s = "";
            foreach(var art in articleList)
            {
                s += art.ToString () + " | ";
            }
            s+="\nEditors: ";
            foreach(var ed in editors)
            {
                s += ed.ToString() + " | ";
            }
            return name + " " + freq.ToString () + " " + date.ToShortDateString () + " " + circ.ToString() +" " +" " + s;
        }
        public virtual string ToShortString () {
            return name + " " + freq.ToString () + " " + date.ToShortDateString () + " " + circ.ToString () + " " + AvRating;
        }
        public override bool Equals (object obj) {
            // If the passed object is null
            if (obj == null) {
                return false;
            }
            if (!(obj is Magazine)) {
                return false;
            }
            return (this.circ == ((Magazine) obj).circ) &&
                (this.name.Equals (((Magazine) obj).name)) &&
                (this.freq == ((Magazine) obj).freq) &&
                (this.date == ((Magazine) obj).date) &&
                (this.articleList == ((Magazine) obj).articleList) &&
                (this.editors == ((Magazine)obj).editors);
        }
        public override int GetHashCode () {
            return name.GetHashCode () ^ freq.GetHashCode () ^ date.GetHashCode () ^ circ.GetHashCode () ^ articleList.GetHashCode () ^ editors.GetHashCode();
        }
        public static bool operator == (Magazine m1, Magazine m2) {
            return m1.Equals (m2);
        }
        public static bool operator != (Magazine m1, Magazine m2) {
            return !m1.Equals (m2);
        }
        // public override object DeepCopy () {
        //     Magazine copy = new Magazine (this.name, this.freq, this.date, this.circ);
        //     copy.ArticleList = this.ArticleList;
        //     copy.Editors = this.Editors;
        //     return copy;
        // }
        public new Magazine DeepCopy()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            Magazine result;
            using (MemoryStream ms = new MemoryStream())
            {
                binForm.Serialize(ms, this);
                ms.Position = 0;
                result = (Magazine)binForm.Deserialize(ms);
            }
            return result;
        }
        public bool Save(string filename)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    binForm.Serialize(fs, this);
                }
                return true;
            }
            catch
            {
                return false;
            }   
        }
        public bool Load(string filename)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            Magazine magazine;
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    magazine = (Magazine)binForm.Deserialize(fs);
                    circ = magazine.circ;
                    name = magazine.name;
                    date = magazine.date;
                    freq = magazine.freq;
                    articleList.Clear();
                    editors.Clear();
                    foreach(Person p in magazine.editors)
                    {
                        editors.Add(new Person(p.Name, p.Surname, p.Birthday));
                    }
                    foreach(Article a in magazine.articleList)
                    {
                        articleList.Add((Article)a.DeepCopy());
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine("Для добавления статьи введите данные в виде следующей строки: Имя Фамилия гггг.мм.дд названиеСтатьи рейтингСтатьи");
                string input = Console.ReadLine();
                string[] data = input.Split(" ");
                Person author = new Person(data[0], data[1], Convert.ToDateTime(data[2]));
                articleList.Add(new Article(author, data[3], Convert.ToDouble(data[4])));
                return true;
            }
            catch
            {
                Console.WriteLine("В вводе были допущены ошибки");
                return false;
            }
        }
        public static bool Save(string filename, Magazine magazine)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            try
            {
                using(FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    binForm.Serialize(fs, magazine);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Load(string filename, ref Magazine magazine)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            try
            {
                using(FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    magazine = (Magazine)binForm.Deserialize(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Edition Edition
        {
            get
            {
                return new Edition(this.Name, this.Date, this.Circ);
            }
            set
            {
                name = value.Name;
                date = value.Date;
                circ = value.Circ;
            }
        }

        public double Rating
        {
            get { return AvRating; }
        }
        public IEnumerable BiggerRating(double n)
        {
            for(int i=0;i<articleList.Count;i++)
            {
                if (articleList[i].Rating > n)
                    yield return articleList[i];
            }
        }
        public IEnumerable LineInName(string line)
        {
            for (int i=0;i<articleList.Count;i++)
            {
                if (articleList[i].articleName.Contains(line))
                    yield return articleList[i];
            }
        }
        public IEnumerable AuthorsInEditors()
        {
            for (int i=0;i<articleList.Count;i++)
            {
                bool authorIsEditor = false;
                for (int j=0;j<editors.Count;j++)
                {
                    if (articleList[i].author == editors[j])
                    {
                        authorIsEditor = true;
                    }
                }
                if(authorIsEditor)
                {
                    yield return articleList[i];
                }
            }
        }
        public IEnumerable EditorsNotInAuthors()
        {
            for (int i=0;i<editors.Count;i++)
            {
                bool editorIsAuthor = false;
                for (int j=0;j<articleList.Count;j++)
                {
                    if (editors[i] == articleList[i].author)
                    {
                        editorIsAuthor = true;
                    }
                }
                if(!editorIsAuthor)
                    yield return editors[i];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(articleList, editors);
        }
    }
}