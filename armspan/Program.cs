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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SaveFilename = "iotest.txt";
            if (File.Exists(SaveFilename))
            {
                JSONCapable.LoadState(File.ReadAllText(SaveFilename));
            }
            Application.Run(new FormMain());
        }

        private static string m_savename;

        public static string SaveFilename
        {
            get { return m_savename; }
            set { m_savename = value; }
        }
        
    }
}
