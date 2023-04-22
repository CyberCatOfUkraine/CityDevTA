using Middleware.Models;
using Project_Sentinel.Command;
using Project_Sentinel.UICustomItem.MessageBox;
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

namespace Project_Sentinel.UICustomItem.ViewDialogWindow.RecordViewDialog
{
    /// <summary>
    /// Interaction logic for EditRecordWindow.xaml
    /// </summary>
    public partial class EditRecordWindow : Window
    {
        private Action<RecordDTO> editAction;
        private readonly RecordDTO recordDTO;

        public EditRecordWindow(Action<RecordDTO> editCommand, IEnumerable<AppDTO> allApps, IEnumerable<UserDTO> allUsers, IEnumerable<CommentDTO> allComments, RecordDTO recordDTO)
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            this.editAction = editCommand;
            this.recordDTO = recordDTO;
            AppComboBox.ItemsSource = allApps;
            UserComboBox.ItemsSource = allUsers;
            CommentComboBox.ItemsSource = allComments;

            CurrentApp = recordDTO.App;
            CurrentUser = recordDTO.User;
            CurrentComment = recordDTO.Comment;
        }

        private static EditRecordWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public static readonly DependencyProperty SelectedAppProperty = DependencyProperty.Register(
        "CurrentApp", typeof(object), typeof(EditAppWindow), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedUserProperty = DependencyProperty.Register(
        "CurrentUser", typeof(object), typeof(EditAppWindow), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedCommentProperty = DependencyProperty.Register(
        "CurrentComment", typeof(object), typeof(EditAppWindow), new PropertyMetadata(default(object)));

        public ICommand EditCommand => new MyCommand((object obj) => { EditRecord(); });
        public RecordDTO CurrentRecord;

        public AppDTO CurrentApp
        {
            get { return (AppDTO)GetValue(SelectedAppProperty); }
            set { SetValue(SelectedAppProperty, value); }
        }

        public UserDTO CurrentUser
        {
            get { return (UserDTO)GetValue(SelectedUserProperty); }
            set { SetValue(SelectedUserProperty, value); }
        }

        public CommentDTO CurrentComment
        {
            get { return (CommentDTO)GetValue(SelectedCommentProperty); }
            set { SetValue(SelectedCommentProperty, value); }
        }

        public bool IsSelected { get; set; }

        private void EditRecord()
        {
            if (CurrentApp == null)
            {
                new OkCancelNotification($"Виберіть застосунок:  !", "Редагування запису", false).Show();
                return;
            }
            if (CurrentUser == null)
            {
                new OkCancelNotification($"Виберіть користувача:  !", "Редагування запису", false).Show();
                return;
            }
            if (CurrentComment == null)
            {
                new OkCancelNotification($"Виберіть коментар:  !", "Редагування запису", false).Show();
                return;
            }

            CustomMessageBox messageBox = new CustomMessageBox("Надані дані буде зв'язано!");
            messageBox.ShowDialog();
            var isOk = messageBox.DialogResult;
            if (isOk.HasValue && isOk.Value)
            {
                CurrentRecord = recordDTO;
                CurrentRecord.App = CurrentApp;
                CurrentRecord.User = CurrentUser;
                CurrentRecord.Comment = CurrentComment;
                editAction.Invoke(CurrentRecord);
                IsSelected = true;
                Instance.Close();
            }
        }
    }
}