// Credits to:
// Matty Southall (Base)
// Uber (Config Data)

using System;
using System.IO;
using System.Net;

namespace HabboSWFDownloader.Downloader{
    
    class MainApp{
        private static string CurrentDirectory;
        private static string UrlExtension;
        private static string Hof_FurniUrl;
        private static string FurniDataUrl;
        private static string ProductDataUrl;

        public static void Initialize(string[] args) {

            Console.Title = "HSD Build 1 (Compiled on 05-02-2014)";

            CurrentDirectory = Environment.CurrentDirectory;
            UrlExtension = "com";
            Hof_FurniUrl = "http://images.habbohotel." + UrlExtension + "/dcr/hof_furni/";
            FurniDataUrl = "http://www.habbo.com" + "/gamedata/furnidata";
            ProductDataUrl = "http://www.habbo.com" + "/gamedata/productdata";

            WriteOut("HabboSWF Downloader - Build 1 - Compiled on 05-02-2014");
            WriteOut("https://github.com/Biblioteca13/Habbo-SWF-Downloader" + "\n");

            if (!Directory.Exists(CurrentDirectory + @"\hof_furni")) {
                WriteOut("Creating hof_furni directory..." + "\n");
                Directory.CreateDirectory(CurrentDirectory + @"\hof_furni");
                WriteOut("Done!" + "\n");
            }

            WebClient Dlr = new WebClient();

            WriteOut("Downloading furnidata and productdata..." + "\n");
            Dlr.DownloadFile(FurniDataUrl, CurrentDirectory + @"\furnidata.txt");
            Dlr.DownloadFile(ProductDataUrl, CurrentDirectory + @"\productdata.txt");
            WriteOut("Done!" + "\n");

            WriteOut("Reading file: " + Hof_FurniUrl + "\n");
            Stream Stream = Dlr.OpenRead(FurniDataUrl);
            WriteOut("Done!" + "\n");
            StreamReader Reader = new StreamReader(Stream);

            int Count = 0;
            string[] Array = Reader.ReadToEnd().Split(new char[] { ']' });

            WriteOut("Starting download of the furniture SWF files...");
            
            for (int i = 0; i < Array.Length; i++) {
                string str = Array[i].Replace("[", "");
                string[] Array2 = Array[i].Replace("\"", "").Split(new char[] { ',' });
                string str2 = "";

                if (Array2.Length >= 4) {
                    try {
                        if (int.Parse(Array2[3]) != 0) {
                            str2 = Array2[2];
                            try {
                                if (!File.Exists(CurrentDirectory + @"\hof_furni\" + str2 + ".swf")) {
                                    Dlr.DownloadFile(Hof_FurniUrl + Array2[3] + "/" + str2 + ".swf", CurrentDirectory + @"\hof_furni\" + str2 + ".swf");
                                    WriteOut("Downloading: \"" + str2 + ".swf\"");
                                    Count++;
                                }
                            } 
                            catch { }
                        }
                    } 
                    catch {
                        if (int.Parse(Array2[2]) != 0) {
                           str2 = Array2[3];
                        try {
                               if (!File.Exists(CurrentDirectory + @"\hof_furni\" + str2 + ".swf")) {
                                    Dlr.DownloadFile(Hof_FurniUrl + Array2[4] + "/" + str2 + ".swf", CurrentDirectory + @"\hof_furni\" + str2 + ".swf");
                                    WriteOut("Downloaded: \"" + str2 + ".swf\"");
                                    Count++;
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

            if (Count > 0) {
                WriteOut(Count + "SWF's were downloaded!");
            } else {
                WriteOut("No SWF's were downloaded!");
            }

            WriteOut("Press any key to close...");
            Console.ReadKey(true);
        }

        public static void WriteOut(string str){
            Console.WriteLine(str);
        }
    }
}
