using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimplyBackup
{
    public class log
    {
        public static List<string> fifo = new List<string>();

        public static void write(string texto)
        {
            try
            {
                StreamWriter fich = new StreamWriter("log.txt", true);
                string fecha = DateTime.Today.ToShortDateString();
                fecha += " " + DateTime.Now.ToLongTimeString();
                fich.WriteLine(fecha + " : " + texto);
                put(fecha + " : " + texto);
                fich.Close();
            }
            catch { }
        }

        public static void write(Exception ex)
        {
            try
            {
                StreamWriter fich = new StreamWriter("log.txt", true);
                string fecha = DateTime.Today.ToShortDateString();
                fecha += " " + DateTime.Now.ToLongTimeString();
                fich.WriteLine(fecha + " Error: " + ex.Message.ToString());
                put(ex.Message.ToString());
                fich.Close();
            }
            catch { }
        }
        public static void deleteLog()
        {
            try
            {
                if (File.Exists("log.txt"))
                {
                    File.Delete("log.txt");
                }
            }
            catch { }
        }

        private static void put(string texto)
        {
            if (fifo.Count() >40)
            {
                fifo.RemoveAt(0);
            }

            fifo.Add(texto);
        }
        
    }
}
