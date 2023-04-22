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
    /// Interaction logic for EditCommentWindow.xaml
    /// </summary>
    public partial class EditCommentWindow : Window
    {
        public EditCommentWindow(CommentDTO comment, Action<CommentDTO> editAction)
        {
            Instance = this;
            DataContext = this;
            this.editAction = editAction;
            CurrentComment = comment;
            InitializeComponent();
        }

        private Action<CommentDTO> editAction;

        private static EditCommentWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public ICommand EditCommand => new MyCommand((object obj) => { EditApp(); });

        public CommentDTO CurrentComment { get; set; }

        private void EditApp()
        {
            CurrentComment.Text = CommentTextBox.Text;
            CurrentComment.TimeStamp = DateTime.Now;
            editAction.Invoke(CurrentComment);
            Instance.Close();
        }
    }
}