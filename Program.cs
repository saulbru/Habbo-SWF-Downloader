using System;
using System.Security.Permissions;
using HabboSWFDownloader.Downloader;

namespace HabboSWFDownloader{
    
    class Program{
       
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        
        static void Main(string[] args) {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
            MainApp.Initialize(args);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args){
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("Error: " + e.ToString());
            Console.ReadKey(true);
        }
    }
}
