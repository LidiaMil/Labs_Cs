using System.Collections.Generic;

namespace lab4
{
    public class ArticleComparer : IComparer<Article>
    {
        public int Compare(Article art1, Article art2)
        {
            return art1.Rating.CompareTo(art2.Rating);
        }
    }
}