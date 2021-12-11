using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;

namespace lab4
{
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
        public override object DeepCopy () {
            Magazine copy = new Magazine (this.name, this.freq, this.date, this.circ);
            copy.ArticleList = this.ArticleList;
            copy.Editors = this.Editors;
            return copy;
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