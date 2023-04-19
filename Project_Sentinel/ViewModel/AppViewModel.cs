using Project_Sentinel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using Middleware.Models;
using Project_Sentinel.UICustomItem.NotificationMessage;
using Project_Sentinel.UICustomItem.ViewDialogWindow.AppViewDialogWindow;

namespace Project_Sentinel.ViewModel
{
    internal class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        private List<AppDTO> _apps;

        public List<AppDTO> Apps
        {
            get { return _apps; }
            set
            {
                if (_apps != value)
                {
                    _apps = value;
                    OnPropertyChanged(nameof(Apps));
                }
            }
        }

        public AppViewModel()
        {
            Apps = App.DBProvider.databaseContext.AppRepository.GetAll().ToList();
        }

        private void ShowTestMessage(string commandName = "TestMessage")
        {
            MessageBox.Show($"This is {commandName}!");
        }

        public ICommand ShowTestMessageCommand => new MyCommand((object obj) => { ShowTestMessage(); });

        public ICommand AddCommand => new MyCommand((object obj) =>
        {
            try
            {
                var addAction = delegate (AppDTO app) { App.DBProvider.databaseContext.AppRepository.Add(app); };
                var addAppWindow = new AddAppWindow(addAction);
                addAppWindow.ShowDialog();
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Додавання нової програми", false).Show();
            }
            new OkCancelNotification("Виконано успішно!", "Додавання нової програми", true).Show();

            Apps = App.DBProvider.databaseContext.AppRepository.GetAll().ToList();
        });

        public ICommand EditCommand => new MyCommand((object obj) => { new OkCancelNotification("test 1", "test", true).Show(); });
        public ICommand DeleteCommand => new MyCommand((object obj) => { new OkCancelNotification("test 2", "toast", false).Show(); });
        //public ICommand ProgramMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new ProgramsView()); });
    }
}