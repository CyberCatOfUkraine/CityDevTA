using Middleware.DBProvider;
using Middleware.Models;
using Project_Sentinel.Command;
using Project_Sentinel.UICustomItem.Button;
using Project_Sentinel.UICustomItem.NotificationMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Sentinel.UICustomItem.ViewDialogWindow.AppViewDialogWindow
{
    /// <summary>
    /// Interaction logic for AddAppWindow.xaml
    /// </summary>
    public partial class AddAppWindow : Window
    {
        private Action<AppDTO> addAction;

        public AddAppWindow(Action<AppDTO> addAction)
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            this.addAction = addAction;
        }

        private static AddAppWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand AddCommand => new MyCommand((object obj) => { AddApp(); });

        private void AddApp()
        {
            addAction.Invoke(new AppDTO(AppNameTextBox.Text)); new OkCancelNotification("test 1", "test", true).Show(); ;
            Instance.Close();
        }
    }
}