using Middleware.Models;
using Project_Sentinel.Command;
using Project_Sentinel.UICustomItem.MessageBox;
using Project_Sentinel.UICustomItem.NotificationMessage;
using Project_Sentinel.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Project_Sentinel.UICustomItem.ViewDialogWindow.RecordViewDialog
{
    /// <summary>
    /// Interaction logic for AddRecordWindow.xaml
    /// </summary>
    public partial class AddRecordWindow : Window
    {
        private Action<RecordDTO> addAction;

        public AddRecordWindow(Action<RecordDTO> addCommand, System.Collections.Generic.IEnumerable<AppDTO> allApps, System.Collections.Generic.IEnumerable<UserDTO> allUsers, System.Collections.Generic.IEnumerable<CommentDTO> allComments)
        {
            InitializeComponent();
            Instance = this;
            DataContext = this;
            this.addAction = addCommand;
            AppComboBox.ItemsSource = allApps;
            UserComboBox.ItemsSource = allUsers;
            CommentComboBox.ItemsSource = allComments;
        }

        private static AddRecordWindow Instance { get; set; }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(() => { Instance?.Close(); });
            }
            set { }
        }

        public static readonly DependencyProperty SelectedAppProperty = DependencyProperty.Register(
        "CurrentApp", typeof(object), typeof(AddRecordWindow), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedUserProperty = DependencyProperty.Register(
        "CurrentUser", typeof(object), typeof(AddRecordWindow), new PropertyMetadata(default(object)));

        public static readonly DependencyProperty SelectedCommentProperty = DependencyProperty.Register(
        "CurrentComment", typeof(object), typeof(AddRecordWindow), new PropertyMetadata(default(object)));

        public ICommand AddCommand => new MyCommand((object obj) => { AddApp(); });
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

        private void AddApp()
        {
            if (CurrentApp == null)
            {
                new OkCancelNotification($"Виберіть застосунок:  !", "Додавання нового запису", false).Show();
                return;
            }
            if (CurrentUser == null)
            {
                new OkCancelNotification($"Виберіть користувача:  !", "Додавання нового запису", false).Show();
                return;
            }
            if (CurrentComment == null)
            {
                new OkCancelNotification($"Виберіть коментар:  !", "Додавання нового запису", false).Show();
                return;
            }

            CustomMessageBox messageBox = new CustomMessageBox("Надані дані буде зв'язано!");
            messageBox.ShowDialog();
            var isOk = messageBox.DialogResult;
            if (isOk.HasValue && isOk.Value)
            {
                CurrentRecord = new RecordDTO(CurrentApp, CurrentUser, CurrentComment);
                addAction.Invoke(CurrentRecord);
                Instance.Close();
            }
        }
    }
}