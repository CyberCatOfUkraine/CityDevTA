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
using Project_Sentinel.UICustomItem.Button;
using DatabaseWorker.Model;
using System.Collections.ObjectModel;

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

        private ObservableCollection<AppDTO> _apps;

        public ObservableCollection<AppDTO> Apps
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
            TryUpdateView();
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
                var addAppWindow = new AddAppWindow(App.DBProvider.databaseContext.AppRepository.Add);
                addAppWindow.ShowDialog();
                if (addAppWindow.CurrentApp != null)
                {
                    new OkCancelNotification("Виконано успішно!", "Додавання нової програми", true).Show();
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Додавання нової програми", false).Show();
            }

            TryUpdateView();
        });

        public ICommand EditCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is AppDTO app)
                {
                    EditAppWindow window = new EditAppWindow(app, App.DBProvider.databaseContext.AppRepository.Update);
                    window.ShowDialog();
                    var modifiedApp = window.CurrentApp;
                    if (modifiedApp != app)
                    {
                        new OkCancelNotification("Виконано успішно!", "Редагування програми", true).Show();
                    }
                }
                else
                {
                    throw new Exception("Вибрана програма має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Редагування програми", false).Show();
            }

            TryUpdateView();
        });

        public ICommand DeleteCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is AppDTO app)
                {
                    App.DBProvider.databaseContext.AppRepository.Delete(app);
                    Apps.Remove(app);
                    new OkCancelNotification("Виконано успішно!", "Видалення програми", true).Show();
                }
                else
                {
                    throw new Exception("Вибрана програма має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Видалення програми", false).Show();
            }
        });

        public void TryUpdateView()
        {
            try
            {
                var apps = App.DBProvider.databaseContext.AppRepository.GetAll();

                var collection = new ObservableCollection<AppDTO>();
                foreach (var item in apps)
                {
                    collection.Add(item);
                }
                Apps = collection;
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Оновлення списку програм", false).Show();
            }
        }
    }
}