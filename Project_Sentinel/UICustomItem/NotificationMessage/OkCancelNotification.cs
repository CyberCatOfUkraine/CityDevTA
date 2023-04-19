using Notification.Wpf;
using Notification.Wpf.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Project_Sentinel.UICustomItem.NotificationMessage
{
    internal class OkCancelNotification
    {
        private static readonly NotificationManager _notificationManager = new();

        //static Notifier() => Resources.Culture = Thread.CurrentThread.CurrentUICulture;
        public OkCancelNotification(string text, string caption, bool isOk)
        {
            Message = text;
            Title = caption;
            IsOk = isOk;
        }

        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsOk { get; set; }

        public void Show()
        {
            string hex = "#44475a";
            Color color = (Color)ColorConverter.ConvertFromString(hex);
            SolidColorBrush Background = new SolidColorBrush(color);

            string hex1 = "#f8f8f2";
            Color color1 = (Color)ColorConverter.ConvertFromString(hex1);
            SolidColorBrush Foreground = new SolidColorBrush(color1);

            string hex2 = "#bd93f9";
            Color color2 = (Color)ColorConverter.ConvertFromString(hex2);
            SolidColorBrush Accent = new SolidColorBrush(color2);

            var content = new NotificationContent
            {
                Title = Title,
                Message = Message,
                RowsCount = 1, //Will show 3 rows and trim after

                Background = IsOk ? Background : Accent,
                Foreground = IsOk ? Foreground : Background
            };

            _notificationManager.Show(content);
        }
    }
}