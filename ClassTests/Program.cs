/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The ClassTests program is used to test classes
 * before adding them to the GUI package.
 * 
 */
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

            List<Span.Occurrence> ocrs = new List<Span.Occurrence>();
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2), DateTime.Today.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2.05), DateTime.Today.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(1.9), DateTime.Today.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2), DateTime.Today.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2.05), DateTime.Today.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2.05), DateTime.Today.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2), DateTime.Today.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(1.9), DateTime.Today.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(1.9), DateTime.Today.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2), DateTime.Today.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(2.1), DateTime.Today.AddDays(3), "none yet"));//*
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(0.5), DateTime.Today.AddDays(1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, DateTime.Today.AddDays(3), DateTime.Today.AddDays(4), "none yet"));
            foreach (Span.Occurrence ocr in ocrs)
            {
                Console.WriteLine(ocr);
            }
            for (int i = 1; i < ocrs.Count; i++)
            {
                Console.WriteLine(ocrs[0].Overlaps(ocrs[i]));
            }
            

        }
    }
}
