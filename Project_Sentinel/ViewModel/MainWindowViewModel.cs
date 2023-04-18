using Project_Sentinel.Command;
using Project_Sentinel.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Sentinel.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void AddViewToViewCollection(UserControl view)
        {
            var grid = Application.Current.MainWindow.FindName("ContainerGrid") as Grid;
            grid?.Children.Clear();
            grid?.Children.Add(view);
        }

        private UserControl _control;

        public UserControl Control
        {
            get => _control;
            set
            {
                _control = value;
                OnPropertyChanged(nameof(Control));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        public MainWindowViewModel()
        {
            Control = new RecordView();
        }

        private void ShowTestMessage(string commandName = "TestMessage")
        {
            MessageBox.Show($"This is {commandName}!");
        }

        public ICommand ShowTestMessageCommand => new MyCommand((object obj) => { ShowTestMessage(); });
        public ICommand ProgramMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new ProgramsView()); });

        public ICommand UsersMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new UsersView()); });
        public ICommand CommentsMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new CommentView()); });
        public ICommand RecordsMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new RecordView()); ; });
    }
}