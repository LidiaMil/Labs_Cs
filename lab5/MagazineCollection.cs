using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;

namespace lab5
{
    class MagazineCollection<TKey>
    {
        public string CollectionName {get; set;}
        private Dictionary<TKey, Magazine> mags = new Dictionary<TKey, Magazine>();
        private KeySelector<TKey> keySelector;
        public MagazineCollection(KeySelector<TKey> _keySelector)
        {
            keySelector = _keySelector;
        }
        public bool Replace(Magazine mold, Magazine mnew)
        {
            foreach(var item in mags)
            {
                if (item.Value == mold)
                {
                    MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(CollectionName, Update.Replace,
                        "", item.Key));
                    mags[item.Key] = mnew;
                    item.Value.PropertyChanged -= MagazinePropertyChanged;
                    mnew.PropertyChanged += MagazinePropertyChanged;
                    return true;
                }
            }
            
            return false;
        }
        public event MagazinesChangedHandler<TKey> MagazinesChanged;
        private void MagazinePropertyChanged(object subject, PropertyChangedEventArgs _propertyChanged)
        {
            if (MagazinesChanged != null)
            {
                MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(CollectionName, Update.Property,
                     _propertyChanged.PropertyName, keySelector((Magazine)subject)));
            }
        }
        public void AddDefaults()
        {
            Magazine mag = new Magazine();
            TKey key = keySelector(mag);
            mags.Add(key, mag);
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(CollectionName, Update.Add, "", key));
            mag.PropertyChanged += MagazinePropertyChanged;
        }
        public void AddMagazines(params Magazine[] magazines)
        {
            for(int i=0; i<magazines.Length; i++)
            {
                TKey key = keySelector(magazines[i]);
                mags.Add(key, magazines[i]);
                MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(CollectionName, Update.Add, "", key));
                magazines[i].PropertyChanged += MagazinePropertyChanged;
            }
        }
        public double maxAvRating
        {
            get
            {
                if(mags.Count > 0) return mags.Values.Max(r => r.AvRating);
                return 0;
            }
        }
        public IEnumerable<KeyValuePair<TKey,Magazine>> FrequencyGroup(Frequency value)
        {
            return mags.Where(x => x.Value.Freq == value);
        }
        public IEnumerable<IGrouping<Frequency,KeyValuePair<TKey,Magazine >>> GroupElements
        {
            get
            {
                return mags.GroupBy(x => x.Value.Freq);
            }
        }
        public override string ToString()
        {
            string s = "";
            foreach(KeyValuePair<TKey, Magazine> kvp in mags)
            {
                s += kvp.Key.ToString() + ":\n" + kvp.Value.ToString() + "\n==============================\n";
            } 
            return s;
        }
        public virtual string ToShortString()
        {
            string s = "";
            foreach(KeyValuePair<TKey, Magazine> kvp in mags)
            {
                s += kvp.Key.ToString() + ":\n" + kvp.Value.ToShortString() + ", число статей "
                     + kvp.Value.ArticleList.Count + ", число редакторов " + kvp.Value.Editors.Count;
            }
            return s;
        }
    }
}