using Middleware.Models;
using Project_Sentinel.Command;
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
    /// Interaction logic for EditAppWindow.xaml
    /// </summary>
    public partial class EditAppWindow : Window
    {
        public EditAppWindow(AppDTO app, Action<AppDTO> editAction)
        {
            InitializeComponent();

            Instance = this;
            DataContext = this;
            this.editAction = editAction;
            CurrentApp = app;
        }

        private Action<AppDTO> editAction;

        private static EditAppWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand EditCommand => new MyCommand((object obj) => { EditApp(); });

        public AppDTO CurrentApp { get; set; }

        private void EditApp()
        {
            CurrentApp.Name= AppNameTextBox.Text;
            editAction.Invoke(CurrentApp);
            Instance.Close();
        }
    }
}