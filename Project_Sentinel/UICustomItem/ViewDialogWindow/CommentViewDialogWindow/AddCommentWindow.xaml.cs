using Middleware.Models;
using Project_Sentinel.Command;
using Project_Sentinel.UICustomItem.ViewDialogWindow.UsersViewDialogWindow;
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

namespace Project_Sentinel.UICustomItem.ViewDialogWindow.CommentViewDialogWindow
{
    /// <summary>
    /// Interaction logic for AddCommentWindow.xaml
    /// </summary>
    public partial class AddCommentWindow : Window
    {
        private Action<CommentDTO> addAction;

        public AddCommentWindow(Action<CommentDTO> addAction)
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            this.addAction = addAction;
        }

        private static AddCommentWindow Instance { get; set; }
        public CommentDTO CurrentComment;

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand AddCommand => new MyCommand((object obj) => { AddComment(); });

        private void AddComment()
        {
            CurrentComment = new CommentDTO(CommentTextBox.Text, DateTime.Now);
            addAction.Invoke(CurrentComment);
            Instance.Close();
        }
    }
}