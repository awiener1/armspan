using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("shsdhf", "Walk to the grocer");
            test.Add("nefivnfivn", "boil onions");
            test.Add("rbvsrtfb", "laugh casually at passersby");
            test.Add("oqwioqi", "think up better test data");

            foreach (KeyValuePair<string, string> kvp in test){
                Console.WriteLine(kvp.Value);
            }

            List<Span.Category> cats = new List<Span.Category>();
            cats.Add(new Span.Category("Business", Color.Pink));
            cats.Add(new Span.Category("Food", Color.Blue));
            cats.Add(new Span.Category("Travel", Color.DarkGreen));
            cats.Add(new Span.Category("Staring at Screens", Color.Orange));

            foreach (Span.Category cat in cats)
            {
                Console.WriteLine(cat);
            }

            Span.Category newcat = (Span.Category)Span.JSONCapable.FromString(cats[0].ToString(), Type.GetType("Span.Category"));
            Console.WriteLine(newcat.Color);

        }
    }
}
