using Project_Sentinel.Command;
using Project_Sentinel.ViewModel;
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

namespace Project_Sentinel.UICustomItem.Button
{
    /// <summary>
    /// Interaction logic for MenuPanelButton.xaml
    /// </summary>
    public partial class MenuPanelButton : UserControl
    {
        public MenuPanelButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty MyTextProperty =
        DependencyProperty.Register("BtnCaption", typeof(string), typeof(MenuPanelButton), new PropertyMetadata(string.Empty));

        public string BtnCaption
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }

        public static readonly DependencyProperty MyCommandProperty = DependencyProperty.Register(
        "BtnCommand",
        typeof(ICommand),
        typeof(MenuPanelButton),
        new PropertyMetadata(null));

        public ICommand BtnCommand
        {
            get { return (ICommand)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnCommand?.Execute(null);
        }
    }
}