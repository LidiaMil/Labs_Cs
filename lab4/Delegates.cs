using System;
using System.Collections.Generic;

namespace lab4
{
    delegate void MagazinesChangedHandler<TKey> (object source, MagazinesChangedEventArgs<TKey> args);
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue> (int j);
    delegate TKey KeySelector<TKey> (Magazine mg);
}