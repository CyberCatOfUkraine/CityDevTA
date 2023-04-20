using Middleware.Models;
using Project_Sentinel.Command;
using Project_Sentinel.UICustomItem.NotificationMessage;
using Project_Sentinel.UICustomItem.ViewDialogWindow.AppViewDialogWindow;
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

namespace Project_Sentinel.UICustomItem.ViewDialogWindow.UsersViewDialogWindow
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private Action<UserDTO> addAction;

        public AddUserWindow(Action<UserDTO> addAction)
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            this.addAction = addAction;
        }

        private static AddUserWindow Instance { get; set; }
        public UserDTO CurrentUser;

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand AddCommand => new MyCommand((object obj) => { AddUser(); });

        private void AddUser()
        {
            CurrentUser = new UserDTO(UserNameTextBox.Text);
            addAction.Invoke(CurrentUser);
            Instance.Close();
        }
    }
}