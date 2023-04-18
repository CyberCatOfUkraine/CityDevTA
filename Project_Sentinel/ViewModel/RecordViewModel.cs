using Project_Sentinel.Command;
using Project_Sentinel.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Project_Sentinel.ViewModel
{
    internal class RecordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        private void ShowTestMessage(string commandName = "TestMessage")
        {
            MessageBox.Show($"This is {commandName}!");
        }

        public ICommand ShowTestMessageCommand => new MyCommand((object obj) => { ShowTestMessage(); });
        //public ICommand ProgramMenuItemCommand => new MyCommand((object obj) => { AddViewToViewCollection(new ProgramsView()); });
    }
}