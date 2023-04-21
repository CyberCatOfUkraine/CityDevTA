using Project_Sentinel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Middleware.Models;
using Project_Sentinel.UICustomItem.NotificationMessage;
using Project_Sentinel.UICustomItem.ViewDialogWindow.AppViewDialogWindow;
using System.Collections.ObjectModel;
using Project_Sentinel.UICustomItem.ViewDialogWindow.CommentViewDialogWindow;

namespace Project_Sentinel.ViewModel
{
    internal class CommentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        private ObservableCollection<CommentDTO> _comments;

        public ObservableCollection<CommentDTO> Comments
        {
            get { return _comments; }
            set
            {
                if (_comments != value)
                {
                    _comments = value;
                    OnPropertyChanged(nameof(Comments));
                }
            }
        }

        public CommentViewModel()
        {
            TryUpdateView();
        }

        private void ShowTestMessage(string commandName = "TestMessage")
        {
            MessageBox.Show($"This is {commandName}!");
        }

        public ICommand ShowTestMessageCommand => new MyCommand((object obj) => { ShowTestMessage(); });

        public ICommand AddCommand => new MyCommand((object obj) =>
        {
            try
            {
                var addCommentWindow = new AddCommentWindow(App.DBProvider.databaseContext.CommentRepository.Add);
                addCommentWindow.ShowDialog();
                if (addCommentWindow.CurrentComment != null)
                {
                    new OkCancelNotification("Виконано успішно!", "Додавання нового коментаря", true).Show();
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Додавання нового коментаря", false).Show();
            }

            TryUpdateView();
        });

        public ICommand EditCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is CommentDTO comment)
                {
                    EditCommentWindow window = new EditCommentWindow(comment, App.DBProvider.databaseContext.CommentRepository.Update);
                    window.ShowDialog();
                    var modifiedComment = window.CurrentComment;
                    if (modifiedComment != comment)
                    {
                        new OkCancelNotification("Виконано успішно!", "Редагування коментаря", true).Show();
                    }
                }
                else
                {
                    throw new Exception("Вибраний коментар має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Редагування коментаря", false).Show();
            }

            TryUpdateView();
        });

        public ICommand DeleteCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is CommentDTO comment)
                {
                    App.DBProvider.databaseContext.CommentRepository.Delete(comment);
                    Comments.Remove(comment);
                    new OkCancelNotification("Виконано успішно!", "Видалення коментаря", true).Show();
                }
                else
                {
                    throw new Exception("Вибраний коментар має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Видалення коментаря", false).Show();
            }
        });

        public void TryUpdateView()
        {
            try
            {
                var comments = App.DBProvider.databaseContext.CommentRepository.GetAll();

                var collection = new ObservableCollection<CommentDTO>();
                foreach (var item in comments)
                {
                    collection.Add(item);
                }
                Comments = collection;
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Оновлення списку коментарів", false).Show();
            }
        }
    }
}