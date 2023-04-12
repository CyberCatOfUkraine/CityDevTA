using Project_Sentinel.Command;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Sentinel.UICustomItem
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