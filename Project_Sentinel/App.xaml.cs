using Middleware.DBProvider;
using Middleware.DBProvider.Test;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Sentinel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SQLiteDBProvider SQLiteDBProvider { get; set; }
        private static FakeDbProvider FakeDbProvider { get; set; }

        public static IDBProvider DBProvider
        {
            get
            {
                var testCallback = delegate () { Console.WriteLine("Db is created"); };
                SQLiteDBProvider ??= new SQLiteDBProvider(testCallback);
                FakeDbProvider ??= new FakeDbProvider(testCallback);
                IDBProvider dbProvider = FakeDbProvider;
                return dbProvider;
            }
        }
    }
}