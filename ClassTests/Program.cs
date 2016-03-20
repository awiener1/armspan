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
using System.IO;
using Span;

namespace ClassTests
{
    class Program
    {

        static void Main(string[] args)
        {
            
            bool makeCat = true;
            while (makeCat)
            {

                Console.Write("Add Category? Y/N");
                makeCat = Console.ReadLine().ToUpper().StartsWith("Y");
                if (!makeCat) break;
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Category c001 = new Category(name, Color.Black);

            }

            Event loado = Event.FromJSON(File.ReadAllText("iotest.txt"));

            Occurrence tdc = loado.Rules[0].Occurrences()[3];

            foreach (Occurrence ocr in loado.Rules[0].Occurrences())
            {
                Console.WriteLine(ocr.StartActual + " -> " + ocr.EndActual + (ocr.Equals(tdc) ? "*" : ""));
            }

            loado.Rules[0].DeChain(tdc);

            for (int i = 0; i < loado.Rules.Count(); i++)
            {
                Console.WriteLine(i);
                foreach (Occurrence ocr in loado.Rules[i].Occurrences())
                {
                    Console.WriteLine(ocr.StartActual + " -> " + ocr.EndActual + (ocr.Equals(tdc) ? "*" : ""));
                }
            }
            Console.WriteLine("manual:");
            foreach (Occurrence ocr in loado.ManualOccurrences)
            {
                Console.WriteLine(ocr.StartActual + " -> " + ocr.EndActual + (ocr.Equals(tdc) ? "*" : ""));
            }

            bool makeEvt = true;
            while (makeEvt)
            {

                Console.Write("Add Event? Y/N");
                makeEvt = Console.ReadLine().ToUpper().StartsWith("Y");
                if (!makeEvt) break;
                MakeEvent();

            }
            File.WriteAllText("iotest.txt", 
                Event.All.Values.ElementAt(0).ToString());

        }
 
        static void MakeEvent()
        {
            Console.WriteLine("Make Event");
            Console.Write("Name: ");

            string name = Console.ReadLine();

            Event e001 = new Event(false, name, new List<Occurrence>(), new List<Period>(), "", new List<string>(), null, "");
            bool makeOcc = true;
            while (makeOcc)
            {

                Console.Write("Add Manual Occurrences? Y/N");
                makeOcc = Console.ReadLine().ToUpper().StartsWith("Y");
                if (!makeOcc) break;
                Occurrence o001 = MakeOccurrence(e001);

            }
            makeOcc = true;
            while (makeOcc)
            {

                Console.Write("Add Periodic Occurrences? Y/N");
                makeOcc = Console.ReadLine().ToUpper().StartsWith("Y");
                if (!makeOcc) break;
                Period p001 = MakePeriod(e001);

            }

            Console.Write("Location: ");
            string loc = Console.ReadLine();
            e001.Location = loc;

            Console.WriteLine("Categories (type numbers):");
            foreach (Category cat in Category.All.Values)
            {
                Console.WriteLine(cat.Number + "\t" + cat.Name);
            }
            string cats = Console.ReadLine();
            List<string> eventcats = new List<string>();
            foreach (string cat in cats.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries))
            {
                eventcats.Add(Category.All.Values.First(x => x.Number == uint.Parse(cat)).Id);
            }
            e001.Categories = eventcats;
            AlarmSettings als = new AlarmSettings("");
            als.Alarms.Add(new Alarm(When.Before, 5, Length.Minutes));
            als.Alarms.Add(new Alarm(When.Before, 10, Length.Minutes));
            als.Alarms.Add(new Alarm(When.Before, 30, Length.Minutes));
            als.Alarms.Add(new Alarm(When.During, 0, Length.Minutes));
            als.Alarms.Add(new Alarm(When.During, 20, Length.Minutes));
            als.Alarms.Add(new Alarm(When.After, 0, Length.Minutes));
            e001.Alarms = als;

            Console.Write("Description: ");
            string desc = Console.ReadLine();
            e001.Description = desc;
        }

        static Period MakePeriod(Event e001)
        {
            Console.WriteLine("Make Periodic Occurrence");
            Console.Write("Frequency in minutes: ");
            uint freq = uint.Parse(Console.ReadLine());
            Console.Write("Start how many days from now: ");
            DateTime startDate = DateTime.Now.AddDays(int.Parse(Console.ReadLine()));
            Console.Write("End how many days from now: ");
            DateTime endDate = DateTime.Now.AddDays(int.Parse(Console.ReadLine()));
            Console.Write("Start how many minutes from now: ");
            DateTime startTime = DateTime.Now.AddMinutes(int.Parse(Console.ReadLine()));
            Console.Write("End how many minutes from now: ");
            DateTime endTime = DateTime.Now.AddMinutes(int.Parse(Console.ReadLine()));
            Console.Write("Length of Occurrences in minutes: ");
            TimeSpan length = TimeSpan.FromMinutes(int.Parse(Console.ReadLine()));
            Period p001 = new Period(freq, Frequency.Minutes, startDate, endDate, startTime, endTime, length, e001.Id);
            e001.Rules.Add(p001);
            return p001;
        }

        static Occurrence MakeOccurrence(Event e001)
        {
            Console.WriteLine("Make Manual Occurrence");
            Console.WriteLine("Start how many minutes from now: ");
            DateTime start = DateTime.Now.AddMinutes(int.Parse(Console.ReadLine()));
            Console.WriteLine("End how many minutes from now: ");
            DateTime end = DateTime.Now.AddMinutes(int.Parse(Console.ReadLine()));
            Occurrence o001 = new Occurrence(false, start, end, e001.Id);
            e001.ManualOccurrences.Add(o001);
            return o001;
        }

        //static void Main(string[] args)
        //{
        //    Dictionary<string, string> test = new Dictionary<string, string>();
        //    test.Add("shsdhf", "Walk to the grocer");
        //    test.Add("nefivnfivn", "boil onions");
        //    test.Add("rbvsrtfb", "laugh casually at passersby");
        //    test.Add("oqwioqi", "think up better test data");

        //    foreach (KeyValuePair<string, string> kvp in test){
        //        Console.WriteLine(kvp.Value);
        //    }

        //    List<Span.Category> cats = new List<Span.Category>();
        //    cats.Add(new Span.Category("Business", Color.Pink));
        //    cats.Add(new Span.Category("Food", Color.Blue));
        //    cats.Add(new Span.Category("Travel", Color.DarkGreen));
        //    cats.Add(new Span.Category("Staring at Screens", Color.Orange));

        //    foreach (Span.Category cat in cats)
        //    {
        //        Console.WriteLine(cat);
        //    }

        //    List<Span.Occurrence> ocrs = new List<Span.Occurrence>();
        //    DateTime isnow = DateTime.Now;
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.15), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.15), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.15), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.08), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.1), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.08), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.1), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.08), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(2.1), isnow.AddDays(3), "none yet"));//*
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(1), isnow.AddDays(2), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(0.5), isnow.AddDays(1), "none yet"));
        //    ocrs.Add(new Span.Occurrence(false, isnow.AddDays(3), isnow.AddDays(4), "none yet"));
        //    foreach (Span.Occurrence ocr in ocrs)
        //    {
        //        Console.WriteLine(ocr);
        //    }
        //    for (int i = 1; i < ocrs.Count; i++)
        //    {
        //        Console.WriteLine(ocrs[0].Overlaps(ocrs[i]));
        //    }

        //    Span.AlarmSettings als = new Span.AlarmSettings(ocrs[0].Id);
        //    als.Alarms.Add(new Span.Alarm(Span.When.Before, 30, Span.Length.Minutes));
        //    als.Alarms.Add(new Span.Alarm(Span.When.Before, 2, Span.Length.Hours));
        //    als.Alarms.Add(new Span.Alarm(Span.When.Before, 1, Span.Length.Days));
        //    als.Alarms.Add(new Span.Alarm(Span.When.During, 30, Span.Length.Minutes));
        //    als.Alarms.Add(new Span.Alarm(Span.When.After, 30, Span.Length.Minutes));

        //    List<DateTime> times = new List<DateTime>(als.AlarmTimes);
        //    foreach (DateTime time in times)
        //    {
        //        Console.WriteLine(time.ToShortDateString() + " " + time.ToShortTimeString());
        //    }

        //    List<Span.Occurrence> ocrpoly = new List<Span.Occurrence>();
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.15), "none yet"));
        //    ocrpoly.Add(new Span.TaskOccurrence(isnow.AddDays(1.9), isnow.AddDays(2.15), "none yet", 3));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.15), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.05), isnow.AddDays(2.08), "none yet"));
        //    ocrpoly.Add(new Span.TaskOccurrence(isnow.AddDays(2.05), isnow.AddDays(2.1), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2), isnow.AddDays(2.1), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.08), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(1.9), isnow.AddDays(2.1), "none yet"));
        //    ocrpoly.Add(new Span.TaskOccurrence(isnow.AddDays(2), isnow.AddDays(2.08), "none yet", 7));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(2.1), isnow.AddDays(3), "none yet"));//*
        //    ocrpoly.Add(new Span.TaskOccurrence(isnow.AddDays(1), isnow.AddDays(2), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(0.5), isnow.AddDays(1), "none yet"));
        //    ocrpoly.Add(new Span.Occurrence(false, isnow.AddDays(3), isnow.AddDays(4), "none yet"));
        //    foreach (Span.Occurrence ocr in ocrpoly)
        //    {
        //        if (ocr.IsTask)
        //        {
        //            ((Span.TaskOccurrence)ocr).DoAgain();
        //        }
        //        Console.WriteLine(ocr);
        //    }
        //    for (int i = 1; i < ocrpoly.Count; i++)
        //    {
        //        Console.WriteLine(ocrpoly[0].Overlaps(ocrpoly[i]));
        //    }

        //    Console.WriteLine("===============");
        //    Span.Period per = new Span.Period(3, Span.Frequency.Days, isnow, isnow.AddMonths(1), new DateTime(1, 1, 1, 8, 30, 0), new DateTime(1, 1, 1, 22, 00, 00), new TimeSpan(0, 40, 0), "no parent");
        //    List<Span.Occurrence> ocrper = new List<Span.Occurrence>(per.Occurrences);
        //    foreach (Span.Occurrence ocr in ocrper)
        //    {
       
        //        Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
        //    }
        //    List<Span.Period> perlist = new List<Span.Period>();
        //    perlist.Add(per);
        //    Console.WriteLine("HENCEFORTH I SUMMON UNTO THIS CONSOLE AN AVALANCHE");
        //    per = new Span.Period(50, Span.Frequency.Minutes, isnow, isnow.AddDays(10), new DateTime(1, 1, 1, 8, 30, 0), new DateTime(1, 1, 1, 22, 00, 00), new TimeSpan(0, 40, 0), "no parent");
        //    ocrper = new List<Span.Occurrence>(per.Occurrences);
        //    foreach (Span.Occurrence ocr in ocrper)
        //    {

        //        Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
        //    }
        //    List <Span.Period> perlist2 = new List<Span.Period>(new Span.Period[]{per});
        //    Console.WriteLine("oh, you don't even know what is happening next");
        //    List<string> catsstring = new List<string>();
        //    foreach (Span.Category cat in cats)
        //    {
        //        catsstring.Add(cat.Id);
        //    }
        //    Span.Event firstEvent = new Span.Event(false, "Visit the Guggenhaggin", ocrpoly, perlist,
        //        "Stockton, NY", catsstring, als, 
        //        "You know, this museum is an amalgamation of two totally different ones. I'd like to see how I intend to visit it.");
        //    foreach (Span.Occurrence ocr in firstEvent.Occurrences)
        //    {
        //        Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
        //    }
        //    Console.WriteLine("now let's identify conflicts");
        //    Span.Event secondEvent = new Span.Event(false, "Trustees Banquet", new List<Span.Occurrence>(), perlist2,
        //        "Roquefort Hall", catsstring, als,
        //        "Bring some desserts - don't forget vegan options too. They will supply utensils; forego taking any. " +
        //        "If you saw this event in the mockup and know what it's named after, I will be very surprised.");
        //    Console.WriteLine(Span.Event.All.Count());
        //    foreach (string outputter in firstEvent.getOverlapping().Item2)
        //    {
        //        if (outputter.StartsWith("e")){
        //            Span.Event thisEvt = Span.Event.All[outputter];
        //            Console.WriteLine("Event -> " + thisEvt.Name);
        //            Console.WriteLine("Conflicting categories: ");
        //            List<string> catprt = new List<string>();
        //            foreach (string id in firstEvent.Categories.Intersect(thisEvt.Categories))
        //            {
        //                catprt.Add(cats.Find(x => x.Id == id).Name);
        //            }
        //            Console.WriteLine(string.Join(", ", catprt));

        //        }
        //        if (outputter.StartsWith("o"))
        //        {
        //            Span.Occurrence thisOcr = Span.Occurrence.All[outputter];
        //            Console.WriteLine("Occurrence -> " + thisOcr.StartActual.ToString() + " - " + thisOcr.EndActual.ToString());
        //        }
        //    }
        //    Console.WriteLine("throw a task into the mix");
        //    Span.TaskEvent thirdEvent = new Span.TaskEvent("Package Boxes of Files", 
        //        new List<Span.Occurrence>(new Span.Occurrence[]{ocrpoly[2]}), 
        //        new List<Span.Period>(), "Da Office", new List<string>(new string[]{cats[2].Id}), als,
        //        "Remember they are moving me to the adjacent building and down two floors.", 4);
        //    Tuple<uint, List<string>> ovrlps = thirdEvent.getOverlapping();
        //    Console.WriteLine(ovrlps.Item2.Count());
        //    foreach (string outputter in ovrlps.Item2)
        //    {
        //        if (outputter.StartsWith("e"))
        //        {
        //            Span.Event thisEvt = Span.Event.All[outputter];
        //            Console.WriteLine("Event -> " + thisEvt.Name);
        //            Console.WriteLine("Conflicting categories: ");
        //            List<string> catprt = new List<string>();
        //            foreach (string id in thirdEvent.Categories.Intersect(thisEvt.Categories))
        //            {
        //                catprt.Add(cats.Find(x => x.Id == id).Name);
        //            }
        //            Console.WriteLine(string.Join(", ", catprt));

        //        }
        //        if (outputter.StartsWith("o"))
        //        {
        //            Span.Occurrence thisOcr = Span.Occurrence.All[outputter];
        //            Console.WriteLine("Occurrence -> " + thisOcr.StartActual.ToString() + " - " + thisOcr.EndActual.ToString());
        //        }
        //    }
        //    perlist[0].TimeUnit = Span.Frequency.Hours;
        //    foreach (Span.Occurrence ocr in perlist[0].Occurrences)
        //    {
        //        Console.WriteLine(ocr.StartIntended.ToString() + " " + ocr.EndIntended.ToString());
        //    }
        //}
    }
}
