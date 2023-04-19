using Middleware.DBProvider;
using Middleware.DBProvider.Test;
using Project_Sentinel.UICustomItem.NotificationMessage;
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
                var successDbCreatedCallback = delegate () { new OkCancelNotification("Виконано успішно!", "Підключення до бази даних", true).Show(); };
                FakeDbProvider ??= new FakeDbProvider(successDbCreatedCallback);
                try
                {
                    SQLiteDBProvider ??= new SQLiteDBProvider(successDbCreatedCallback);

                    IDBProvider dbProvider = FakeDbProvider;
                    return dbProvider;
                }
                catch (Exception e)
                {
                    new OkCancelNotification($"Виникла помилка: {e.Message} !", "Підключення до бази даних", false).Show();
                }
                return FakeDbProvider;
            }
        }
    }
}