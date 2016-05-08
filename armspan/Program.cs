/**
 * @file
 * @author Allan Wiener
 * 
 * @section DESCRIPTION
 * 
 * The Program class is the entry point for the application,
 * and is where the filename of the schedule is kept.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Span.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //with visual styles on, has different behavior
            //across Windows themes. But with styles off,
            //the calendar does not work properly.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //load file
            SaveFilename = "schedule.spn";
            if (File.Exists(SaveFilename))
            {
                JSONCapable.LoadState(File.ReadAllText(SaveFilename));
            }
            Application.Run(new FormMain());
        }

        /**
         * The name of the file to load and save. See also SaveFileName.
         */
        private static string m_savename;

        /**
         * Gets or sets the name of the file to load and save.
         * 
         * This property is static rather than constant so that,
         * if an options feature is added to the program,
         * it can be changed from a configuration file.
         */
        public static string SaveFilename
        {
            get { return m_savename; }
            set { m_savename = value; }
        }
        
    }
}
