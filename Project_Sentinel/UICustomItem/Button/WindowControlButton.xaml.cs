using System;
using System.Windows;
using System.Windows.Controls;

namespace Project_Sentinel.UICustomItem.Button
{
    /// <summary>
    /// Interaction logic for WindowControlButton.xaml
    /// </summary>
    public partial class WindowControlButton : UserControl
    {
        public WindowControlButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Text
        { get => _Text; set { _Text = value; } }

        private string _Text;

        public Action ButtonAction { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonAction.Invoke();
        }
    }
}