using DatabaseWorker.Model;
using Middleware.Models;
using Project_Sentinel.Command;
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
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public EditUserWindow(UserDTO user, Action<UserDTO> editAction)
        {
            Instance = this;
            DataContext = this;
            this.editAction = editAction;
            CurrentUser = user;
            InitializeComponent();
        }

        private Action<UserDTO> editAction;

        private static EditUserWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand EditCommand => new MyCommand((object obj) => { EditApp(); });

        public UserDTO CurrentUser { get; set; }

        private void EditApp()
        {
            CurrentUser = new UserDTO(UserNameTextBox.Text);
            editAction.Invoke(CurrentUser);
            Instance.Close();
        }
    }
}