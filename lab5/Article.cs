using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    [Serializable]
    public class Article : IRateAndCopy, IComparable, IComparer<Article>
    {
        public Person author { get; set; }
        public string articleName { get; set; }
        public double rating { get; set; }
        public Article (Person _author, string _articleName, double _rating) {
            author = _author;
            articleName = _articleName;
            rating = _rating;
        }
        public Article () {
            author = new Person ();
            articleName = "DefaultItem";
            rating = 5.5;
        }
        public override string ToString () {
            return author.ToString () + " " + articleName + " " + rating.ToString ();
        }
        public override bool Equals (object obj) {
            // If the passed object is null
            if (obj == null) {
                return false;
            }
            if (!(obj is Person)) {
                return false;
            }
            return (this.author == ((Article) obj).author) &&
                (this.articleName.Equals (((Article) obj).articleName)) &&
                (this.rating == ((Article) obj).rating);
        }
        public override int GetHashCode () {
            return author.GetHashCode () ^ articleName.GetHashCode () ^ rating.GetHashCode ();
        }
        public static bool operator == (Article a1, Article a2) {
            return a1.Equals (a2);
        }
        public static bool operator != (Article a1, Article a2) {
            return !a1.Equals (a2);
        }
        public object DeepCopy () {
            return new Article (this.author, this.articleName, this.rating);
        }
        public double Rating
        {
            get
            {
                return rating;
            }
        }
        public int Compare(Article art1, Article art2)
        {
            return art1.author.Surname.CompareTo(art2.author.Surname);
        }
        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;
            Article otherArticle = obj as Article;
            if(otherArticle != null)
                return this.articleName.CompareTo(otherArticle.articleName);
            else
                throw new ArgumentException("Object is not Article!");
        }
    }
}