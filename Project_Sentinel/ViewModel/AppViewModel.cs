using Project_Sentinel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using Middleware.Models;

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
            var app = new AppDTO("App");
            App.DBProvider.databaseContext.AppRepository.Add(app);
            Apps = App.DBProvider.databaseContext.AppRepository.GetAll().ToList();
        });

        public ICommand EditCommand => new MyCommand((object obj) => { ShowTestMessage(); });
        public ICommand DeleteCommand => new MyCommand((object obj) => { ShowTestMessage(); });
        //public ICommand ProgramMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new ProgramsView()); });
    }
}