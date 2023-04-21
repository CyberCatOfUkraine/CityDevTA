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
using Project_Sentinel.UICustomItem.ViewDialogWindow.UsersViewDialogWindow;

namespace Project_Sentinel.ViewModel
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        private ObservableCollection<UserDTO> _users;

        public ObservableCollection<UserDTO> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public UserViewModel()
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
                var addUserWindow = new AddUserWindow(App.DBProvider.databaseContext.UserRepository.Add);
                addUserWindow.ShowDialog();
                if (addUserWindow.CurrentUser != null)
                {
                    new OkCancelNotification("Виконано успішно!", "Додавання нового користувача", true).Show();
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Додавання нового користувача", false).Show();
            }

            TryUpdateView();
        });

        public ICommand EditCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is UserDTO user)
                {
                    EditUserWindow window = new EditUserWindow(user, App.DBProvider.databaseContext.UserRepository.Update);
                    window.ShowDialog();
                    var modifiedUser = window.CurrentUser;
                    if (modifiedUser != user)
                    {
                        new OkCancelNotification("Виконано успішно!", "Редагування користувача", true).Show();
                    }
                }
                else
                {
                    throw new Exception("Вибраний користувач має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Редагування користувача", false).Show();
            }

            TryUpdateView();
        });

        public ICommand DeleteCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is UserDTO user)
                {
                    var isUserPresent = false;
                    var allUsers = App.DBProvider.databaseContext.UserRepository.GetAll().ToList();
                    foreach (var record in App.DBProvider.databaseContext.RecordRepository.GetAll())
                    {
                        if (allUsers.ToList().Exists(x => x.Id == record.User.Id))
                        {
                            isUserPresent = true;
                        }
                    }
                    if (isUserPresent)
                    {
                        new OkCancelNotification($"Користувач пов'язаний з записом, видалість запис якщо потрібно видалити користувача !", "Видалення користувача", false).Show();
                        return;
                    }
                    App.DBProvider.databaseContext.UserRepository.Delete(user);
                    Users.Remove(user);
                    new OkCancelNotification("Виконано успішно!", "Видалення користувача", true).Show();
                }
                else
                {
                    throw new Exception("Вибраний користувач має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Видалення користувача", false).Show();
            }
        });

        public void TryUpdateView()
        {
            try
            {
                var users = App.DBProvider.databaseContext.UserRepository.GetAll();

                var collection = new ObservableCollection<UserDTO>();
                foreach (var item in users)
                {
                    collection.Add(item);
                }
                Users = collection;
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Оновлення списку користувачів", false).Show();
            }
        }
    }
}