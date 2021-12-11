using System.Collections.Generic;
using System.Linq;
using System;

namespace lab5
{
    class TestCollections<TKey, TValue>
    {
        private List<TKey> listKeys = new List<TKey>();
        private List<string> listStr = new List<string>();
        private Dictionary<TKey, TValue> dictKeys = new Dictionary<TKey, TValue>();
        private Dictionary<string, TValue> dictStr = new Dictionary<string, TValue>();
        GenerateElement<TKey, TValue> generate;
        
        public TestCollections(int count, GenerateElement<TKey, TValue> _generate)
        {
            if (count < 1) throw new ArgumentException("Неверное количество элементов");
            generate = _generate;
            for (int i=0;i<count;i++)
            {
                var item = generate(i);
                dictKeys.Add(item.Key, item.Value);
                dictStr.Add(item.Key.ToString(), item.Value);
                listKeys.Add(item.Key);
                listStr.Add(item.Key.ToString());
            }
        }
        public void searchInListKeys()
        {
            TKey first_key = listKeys[0];
            TKey middle_key = listKeys[listKeys.Count / 2];
            TKey last_key = listKeys[listKeys.Count - 1];
            TKey diff_key = generate(listKeys.Count + 1).Key;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Поиск в List<TKey>");
            sw.Start();
            listKeys.Contains(first_key);
            sw.Stop();
            Console.WriteLine("Первый элемент: " + sw.Elapsed);
            sw.Restart();
            listKeys.Contains(middle_key);
            sw.Stop();
            Console.WriteLine("Средний элемент: " + sw.Elapsed);
            sw.Restart();
            listKeys.Contains(last_key);
            sw.Stop();
            Console.WriteLine("Последний элемент: " + sw.Elapsed);
            sw.Restart();
            listKeys.Contains(diff_key);
            sw.Stop();
            Console.WriteLine("Невходящий элемент: " + sw.Elapsed);
        }
        public void searchInListStr()
        {
            string first_key = listStr[0];
            string middle_key = listStr[listStr.Count / 2];
            string last_key = listStr[listStr.Count - 1];
            string diff_key = generate(listStr.Count + 1).Key.ToString();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Поиск в List<string>");
            sw.Start();
            listStr.Contains(first_key);
            sw.Stop();
            Console.WriteLine("Первый элемент: " + sw.Elapsed);
            sw.Restart();
            listStr.Contains(middle_key);
            sw.Stop();
            Console.WriteLine("Средний элемент: " + sw.Elapsed);
            sw.Restart();
            listStr.Contains(last_key);
            sw.Stop();
            Console.WriteLine("Последний элемент: " + sw.Elapsed);
            sw.Restart();
            listStr.Contains(diff_key);
            sw.Stop();
            Console.WriteLine("Невходящий элемент: " + sw.Elapsed);
        }
        public void searchInDictKeys()
        {
            TKey first_key = dictKeys.ElementAt(0).Key;
            TKey middle_key = dictKeys.ElementAt(dictKeys.Count / 2).Key;
            TKey last_key = dictKeys.ElementAt(dictKeys.Count - 1).Key;
            TKey diff_key = generate(dictKeys.Count + 1).Key;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Поиск по ключам в Dictionary<TKey, TValue>");
            sw.Start();
            dictKeys.ContainsKey(first_key);
            sw.Stop();
            Console.WriteLine("Первый элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsKey(middle_key);
            sw.Stop();
            Console.WriteLine("Средний элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsKey(last_key);
            sw.Stop();
            Console.WriteLine("Последний элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsKey(diff_key);
            sw.Stop();
            Console.WriteLine("Невходящий элемент: " + sw.Elapsed);
        }
        public void searchInDictStr()
        {
            string first_key = dictStr.ElementAt(0).Key;
            string middle_key = dictStr.ElementAt(dictStr.Count / 2).Key;
            string last_key = dictStr.ElementAt(dictStr.Count - 1).Key;
            string diff_key = generate(dictStr.Count + 1).Key.ToString();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Поиск по ключам в Dictionary<string, TValue>");
            sw.Start();
            dictStr.ContainsKey(first_key);
            sw.Stop();
            Console.WriteLine("Первый элемент: " + sw.Elapsed);
            sw.Restart();
            dictStr.ContainsKey(middle_key);
            sw.Stop();
            Console.WriteLine("Средний элемент: " + sw.Elapsed);
            sw.Restart();
            dictStr.ContainsKey(last_key);
            sw.Stop();
            Console.WriteLine("Последний элемент: " + sw.Elapsed);
            sw.Restart();
            dictStr.ContainsKey(diff_key);
            sw.Stop();
            Console.WriteLine("Невходящий элемент: " + sw.Elapsed);
        }
        public void searchInDictKeysByValues()
        {
            TValue first_key = dictKeys.ElementAt(0).Value;
            TValue middle_key = dictKeys.ElementAt(dictKeys.Count / 2).Value;
            TValue last_key = dictKeys.ElementAt(dictKeys.Count - 1).Value;
            TValue diff_key = generate(dictKeys.Count + 1).Value;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Поиск по значениям в Dictionary<TKey, TValue>");
            sw.Start();
            dictKeys.ContainsValue(first_key);
            sw.Stop();
            Console.WriteLine("Первый элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsValue(middle_key);
            sw.Stop();
            Console.WriteLine("Средний элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsValue(last_key);
            sw.Stop();
            Console.WriteLine("Последний элемент: " + sw.Elapsed);
            sw.Restart();
            dictKeys.ContainsValue(diff_key);
            sw.Stop();
            Console.WriteLine("Невходящий элемент: " + sw.Elapsed);
        }
    }
}