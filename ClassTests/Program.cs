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
            DateTime isnow = DateTime.Now;
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.15), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.08), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.1), isnow.AddDays(3), "none yet"));//*
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1), isnow.AddDays(2), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(0.5), isnow.AddDays(1), "none yet"));
            ocrs.Add(new Span.Occurrence(false, isnow.AddDays(3), isnow.AddDays(4), "none yet"));
            foreach (Span.Occurrence ocr in ocrs)
            {
                Console.WriteLine(ocr);
            }
            for (int i = 1; i < ocrs.Count; i++)
            {
                Console.WriteLine(ocrs[0].Overlaps(ocrs[i]));
            }

            Span.AlarmSettings als = new Span.AlarmSettings(ocrs[0].Id);
            als.Alarms.Add(new Span.Alarm(Span.When.Before, 30, Span.Length.Minutes));
            als.Alarms.Add(new Span.Alarm(Span.When.Before, 2, Span.Length.Hours));
            als.Alarms.Add(new Span.Alarm(Span.When.Before, 1, Span.Length.Days));
            als.Alarms.Add(new Span.Alarm(Span.When.During, 30, Span.Length.Minutes));
            als.Alarms.Add(new Span.Alarm(Span.When.After, 30, Span.Length.Minutes));

            List<DateTime> times = new List<DateTime>(als.AlarmTimes);
            foreach (DateTime time in times)
            {
                Console.WriteLine(time.ToShortDateString() + " " + time.ToShortTimeString());
            }

            List<Span.Occurrence> ocrpoly = new List<Span.Occurrence>();
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.15), "none yet"));
            ocrpoly.Add(new Span.TaskOccurrence(true, isnow.AddDays(1.9), isnow.AddDays(2.15), "none yet", 3));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.15), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.08), "none yet"));
            ocrpoly.Add(new Span.TaskOccurrence(true, isnow.AddDays(2.05), isnow.AddDays(2.1), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.08), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.1), "none yet"));
            ocrpoly.Add(new Span.TaskOccurrence(true, isnow.AddDays(2), isnow.AddDays(2.08), "none yet", 7));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.1), isnow.AddDays(3), "none yet"));//*
            ocrpoly.Add(new Span.TaskOccurrence(true, isnow.AddDays(1), isnow.AddDays(2), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(0.5), isnow.AddDays(1), "none yet"));
            ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(3), isnow.AddDays(4), "none yet"));
            foreach (Span.Occurrence ocr in ocrpoly)
            {
                if (ocr.IsTask)
                {
                    ((Span.TaskOccurrence)ocr).DoAgain();
                }
                Console.WriteLine(ocr);
            }
            for (int i = 1; i < ocrpoly.Count; i++)
            {
                Console.WriteLine(ocrpoly[0].Overlaps(ocrpoly[i]));
            }

            Console.WriteLine("===============");
            Span.Period per = new Span.Period(3, Span.Frequency.Days, isnow, isnow.AddMonths(1), new DateTime(1, 1, 1, 8, 30, 0), new DateTime(1, 1, 1, 22, 00, 00), new TimeSpan(0, 40, 0), "no parent");
            List<Span.Occurrence> ocrper = per.Occurrences;
            foreach (Span.Occurrence ocr in ocrper)
            {
       
                Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
            }
            Console.WriteLine("HENCEFORTH I SUMMON UNTO THIS CONSOLE AN AVALANCHE");
            per = new Span.Period(50, Span.Frequency.Minutes, isnow, isnow.AddDays(10), new DateTime(1, 1, 1, 8, 30, 0), new DateTime(1, 1, 1, 22, 00, 00), new TimeSpan(0, 40, 0), "no parent");
            ocrper = per.Occurrences;
            foreach (Span.Occurrence ocr in ocrper)
            {

                Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
            }


        }
    }
}
