using System;
using System.Collections;
using System.Collections.Generic;


namespace lab4
{
    public class MagazineEnumerator : IEnumerator
    {
        List<Article> articles;
        List<Person> editors;
        int idx = -1;
        public MagazineEnumerator(List<Article> _articles, List<Person> _editors)
        {
            articles = _articles;
            editors = _editors;
        }
        public bool MoveNext()
        {
            for (; ; )
            {
                idx++;
                if (idx == articles.Count) return false;
                if (!editors.Contains(articles[idx].author))
                {
                    return true;
                }
            }
        }
        public object Current
        {
            get
            {
                return articles[idx];
            }
        }
        public void Reset()
        {
            idx = -1;   
        }
    }
}