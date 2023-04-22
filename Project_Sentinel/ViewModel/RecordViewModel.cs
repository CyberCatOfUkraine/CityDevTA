using Project_Sentinel.Command;
using Project_Sentinel.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Middleware.Models;
using Project_Sentinel.UICustomItem.NotificationMessage;
using Project_Sentinel.UICustomItem.ViewDialogWindow.AppViewDialogWindow;
using System.Collections.ObjectModel;
using Project_Sentinel.UICustomItem.ViewDialogWindow.RecordViewDialog;
using Project_Sentinel.UICustomItem.MessageBox;

namespace Project_Sentinel.ViewModel
{
    internal class RecordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
#if DEBUG
            Debug.WriteLine($"PropertyChanged: {propertyName}" + " at " + DateTime.Now);
#endif
        }

        private ObservableCollection<RecordDTO> _records;

        public ObservableCollection<RecordDTO> Records
        {
            get { return _records; }
            set
            {
                if (_records != value)
                {
                    _records = value;
                    OnPropertyChanged(nameof(Records));
                }
            }
        }

        public RecordViewModel()
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
                var addCommand = App.DBProvider.databaseContext.RecordRepository.Add;

                var allRecords = App.DBProvider.databaseContext.RecordRepository.GetAll();

                var allRecordsApps = allRecords.Select(x => x.App);
                var allRecordsUsers = allRecords.Select(x => x.User);
                var allRecordsComments = allRecords.Select(x => x.Comment);

                var delAllApps = delegate ()
                {
                    var list = new List<AppDTO>();
                    foreach (var item in App.DBProvider.databaseContext.AppRepository.GetAll())
                    {
                        if (allRecordsApps.FirstOrDefault(x => x.Id == item.Id) == null)
                        {
                            list.Add(item);
                        }
                    }
                    return list;
                };
                var delAllUsers = delegate ()
                {
                    var list = new List<UserDTO>();
                    foreach (var item in App.DBProvider.databaseContext.UserRepository.GetAll())
                    {
                        if (allRecordsUsers.FirstOrDefault(x => x.Id == item.Id) == null)
                        {
                            list.Add(item);
                        }
                    }
                    return list;
                };
                var delAllComments = delegate ()
                {
                    var list = new List<CommentDTO>();
                    foreach (var item in App.DBProvider.databaseContext.CommentRepository.GetAll())
                    {
                        if (allRecordsComments.FirstOrDefault(x => x.Id == item.Id) == null)
                        {
                            list.Add(item);
                        }
                    }
                    return list;
                };
                var allApps = delAllApps.Invoke();

                var allUsers = delAllUsers.Invoke();

                var allComments = delAllComments.Invoke();
                var addRecordWindow = new AddRecordWindow(addCommand, allApps, allUsers, allComments);
                addRecordWindow.ShowDialog();
                if (addRecordWindow.CurrentRecord != null)
                {
                    new OkCancelNotification("Виконано успішно!", "Додавання нового запису", true).Show();
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Додавання нового запису", false).Show();
            }
            TryUpdateView();
        });

        public ICommand EditCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is RecordDTO record)
                {
                    var allRecords = App.DBProvider.databaseContext.RecordRepository.GetAll();

                    var allRecordsApps = allRecords.Select(x => x.App);
                    var allRecordsUsers = allRecords.Select(x => x.User);
                    var allRecordsComments = allRecords.Select(x => x.Comment);

                    var delAllApps = delegate ()
                    {
                        var list = new List<AppDTO>();
                        foreach (var item in App.DBProvider.databaseContext.AppRepository.GetAll())
                        {
                            if (allRecordsApps.FirstOrDefault(x => x.Id == item.Id) == null)
                            {
                                list.Add(item);
                            }
                        }
                        return list;
                    };
                    var delAllUsers = delegate ()
                    {
                        var list = new List<UserDTO>();
                        foreach (var item in App.DBProvider.databaseContext.UserRepository.GetAll())
                        {
                            if (allRecordsUsers.FirstOrDefault(x => x.Id == item.Id) == null)
                            {
                                list.Add(item);
                            }
                        }
                        return list;
                    };
                    var delAllComments = delegate ()
                    {
                        var list = new List<CommentDTO>();
                        foreach (var item in App.DBProvider.databaseContext.CommentRepository.GetAll())
                        {
                            if (allRecordsComments.FirstOrDefault(x => x.Id == item.Id) == null)
                            {
                                list.Add(item);
                            }
                        }
                        return list;
                    };
                    var allApps = delAllApps.Invoke();

                    var allUsers = delAllUsers.Invoke();

                    var allComments = delAllComments.Invoke();

                    EditRecordWindow window = new EditRecordWindow(App.DBProvider.databaseContext.RecordRepository.Update, allApps, allUsers, allComments, record);
                    window.ShowDialog();
                    var selected = window.IsSelected;
                    if (selected)
                    {
                        new OkCancelNotification("Виконано успішно!", "Редагування запису", true).Show();
                    }
                }
                else
                {
                    throw new Exception("Вибраний запис має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Редагування запису", false).Show();
            }

            TryUpdateView();
        });

        public ICommand DeleteCommand => new MyCommand((object obj) =>
        {
            try
            {
                if (obj is RecordDTO app)
                {
                    var isAppPresent = false;
                    var allApps = App.DBProvider.databaseContext.RecordRepository.GetAll().ToList();

                    var window = new CustomMessageBox("Запис зі зв'язаними даними буде видалений!");
                    window.ShowDialog();
                    if (window.DialogResult.HasValue && window.DialogResult.Value)
                    {
                        App.DBProvider.databaseContext.RecordRepository.Delete(app);
                        Records.Remove(app);
                        new OkCancelNotification("Виконано успішно!", "Видалення запису", true).Show();
                    }
                }
                else
                {
                    throw new Exception("Вибраний запис має пусте значення");
                }
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Видалення запису", false).Show();
            }
        });

        public void TryUpdateView()
        {
            try
            {
                var apps = App.DBProvider.databaseContext.RecordRepository.GetAll();

                var collection = new ObservableCollection<RecordDTO>();
                foreach (var item in apps)
                {
                    collection.Add(item);
                }
                Records = collection;
            }
            catch (Exception e)
            {
                new OkCancelNotification($"Виникла помилка: {e.Message} !", "Оновлення списку записів", false).Show();
            }
        }
    }
}