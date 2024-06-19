using System;
using System.IO;
using System.Threading;

namespace Step2Coder_MUTEX
{
    internal class Program
        
    {   //simulieren von MUTEX absichern dass nicht gleichzeitig auf geschrieben und gelesen wird (selbe datei)
        //mittels MUTEX und C#
        //überblick über methoden in MUTEX
        private static Mutex m1 = new Mutex();

        public static void nothingspezial()
        {
            string antwort;
            
            Console.WriteLine(Thread.CurrentThread.Name + " wartet auf den Mutex...");
            m1.WaitOne();
            Console.WriteLine(Thread.CurrentThread.Name + " hat den Mutex erhalten.");
            Console.WriteLine("Was möchtest du schreiben?");
            antwort = Console.ReadLine();
            string dateipfad = @"C:\\Users\\FP2402384\\Desktop\\test";
            try
            {


                // Datei öffnen, schreiben und schließen
                using (StreamWriter sw = new StreamWriter(dateipfad, true))
                {
                    sw.WriteLine(antwort +" von "+Thread.CurrentThread.Name);
                }

                Console.WriteLine(Thread.CurrentThread.Name + " hat in die Datei geschrieben.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ein Fehler ist aufgetreten: " + ex.Message);
            }
            finally
            {
                Thread.Sleep(100);
                Console.WriteLine(Thread.CurrentThread.Name + " gibt den Mutex frei.");

                m1.ReleaseMutex();
            }



           
        }

        static void Main(string[] args)
        {

            for (int i = 0; i < 5; i++)
            {
                string Name;
                Name = Console.ReadLine();
                Thread thread = new Thread(new ThreadStart(nothingspezial));
                thread.Start();
            }


            // Console.WriteLine("Hello World!");
        }
    }
}
