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
    /// Interaction logic for OrdinaryButton.xaml
    /// </summary>
    public partial class OrdinaryButton : UserControl
    {
        public OrdinaryButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty MyTextProperty =
        DependencyProperty.Register("BtnOCaption", typeof(string), typeof(OrdinaryButton), new PropertyMetadata(string.Empty));

        public string BtnOCaption
        {
            get { return (string)GetValue(MyTextProperty); }
            set { SetValue(MyTextProperty, value); }
        }

        public static readonly DependencyProperty MyCommandProperty = DependencyProperty.Register(
        "BtnOCommand",
        typeof(ICommand),
        typeof(OrdinaryButton),
        new PropertyMetadata(null));

        public ICommand BtnOCommand
        {
            get { return (ICommand)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BtnOCommand?.Execute(null);
        }
    }
}