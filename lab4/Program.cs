using System;
using System.Collections.Generic;

namespace lab4
{
    class Program {

        static void Main (string[] args) {
            KeySelector<string> selector = delegate (Magazine input)
            {
                return (input.GetHashCode().ToString());
            };
            MagazineCollection<string> OneCollection = new MagazineCollection<string>(selector);
            MagazineCollection<string> TwoCollection = new MagazineCollection<string>(selector);
            OneCollection.CollectionName = "OneCollection"; 
            TwoCollection.CollectionName = "TwoCollection";
            Listener listener = new Listener();
            OneCollection.MagazinesChanged += listener.newListEntry;
            TwoCollection.MagazinesChanged += listener.newListEntry;
            Magazine OneMagazine = new Magazine("One Title", Frequency.Yearly, new DateTime(), 111);
            Magazine TwoMagazine = new Magazine("Two Title", Frequency.Monthly, new DateTime(2001, 10, 12), 222);
            Magazine ThreeMagazine = new Magazine("Three Title", Frequency.Yearly, new DateTime(1999, 6, 6), 333);
            Magazine FourMagazine = new Magazine("Four Title", Frequency.Monthly, new DateTime(2021, 1, 4), 444);
            Magazine FiveMagazinee = new Magazine("Five Title", Frequency.Yearly, new DateTime(2021, 2, 9), 555);
            Magazine SixMagazine = new Magazine("Six Title", Frequency.Weekly, new DateTime(2025, 3, 7), 666);
            OneCollection.AddMagazines(OneMagazine, TwoMagazine);
            TwoCollection.AddMagazines(ThreeMagazine, FourMagazine);
            TwoCollection.AddDefaults();
            OneMagazine.Circ = 1234;
            ThreeMagazine.Circ = 4321;
            OneCollection.Replace(OneMagazine, FiveMagazinee);
            TwoCollection.Replace(ThreeMagazine, SixMagazine);
            ThreeMagazine.Circ = 1001;
            FiveMagazinee.Date = new DateTime();
            FiveMagazinee.Freq = Frequency.Weekly;
            SixMagazine.Freq = Frequency.Yearly;

            Console.WriteLine(listener.ToString());
        }
    }
}