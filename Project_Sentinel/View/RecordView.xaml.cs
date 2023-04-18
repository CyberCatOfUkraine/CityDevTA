using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Project_Sentinel.View
{
    /// <summary>
    /// Interaction logic for RecordView.xaml
    /// </summary>
    public partial class RecordView : UserControl
    {
        public RecordView()
        {
            InitializeComponent();
            var listOfProducts = new List<Product>();
            listOfProducts.Add(new Product("Dell XPS 13", "Solid laptop for work", "MichaelRobinson"));
            listOfProducts.Add(new Product("MacBook Pro", "Excellent design and performance", "EmilyDavis"));
            listOfProducts.Add(new Product("Lenovo ThinkPad X1 Carbon", "Built like a tank", "DavidLee"));
            listOfProducts.Add(new Product("Canon EOS R", "Excellent camera for photography", "KatieClark"));
            listOfProducts.Add(new Product("Nikon D850", "Great for shooting in low light", "BrianSmith"));
            listOfProducts.Add(new Product("Sony a7 III", "Excellent video capabilities", "OliviaTaylor"));
            listOfProducts.Add(new Product("Fitbit Charge 3", "Tracks heart rate and activity", "AveryJones"));
            listOfProducts.Add(new Product("Apple Watch Series 4", "Great for notifications and apps", "JonathanKim"));
            listOfProducts.Add(new Product("Samsung Gear Sport", "Nice design and features", "AshleyNguyen"));
            listOfProducts.Add(new Product("Dell XPS 13", "Solid laptop for work", "MichaelRobinson"));
            listOfProducts.Add(new Product("MacBook Pro", "Excellent design and performance", "EmilyDavis"));
            listOfProducts.Add(new Product("Lenovo ThinkPad X1 Carbon", "Built like a tank", "DavidLee"));
            listOfProducts.Add(new Product("Canon EOS R", "Excellent camera for photography", "KatieClark"));
            listOfProducts.Add(new Product("Nikon D850", "Great for shooting in low light", "BrianSmith"));
            listOfProducts.Add(new Product("Sony a7 III", "Excellent video capabilities", "OliviaTaylor"));
            listOfProducts.Add(new Product("Fitbit Charge 3", "Tracks heart rate and activity", "AveryJones"));
            listOfProducts.Add(new Product("Apple Watch Series 4", "Great for notifications and apps", "JonathanKim"));
            listOfProducts.Add(new Product("Samsung Gear Sport", "Nice design and features", "AshleyNguyen"));

            listOfProducts.Add(new Product("--------", "------", "---------"));

            listOfProducts.Add(new Product("Dell XPS 13", "Solid laptop for work", "MichaelRobinson"));
            listOfProducts.Add(new Product("MacBook Pro", "Excellent design and performance", "EmilyDavis"));
            listOfProducts.Add(new Product("Lenovo ThinkPad X1 Carbon", "Built like a tank", "DavidLee"));
            listOfProducts.Add(new Product("Canon EOS R", "Excellent camera for photography", "KatieClark"));
            listOfProducts.Add(new Product("Nikon D850", "Great for shooting in low light", "BrianSmith"));
            listOfProducts.Add(new Product("Sony a7 III", "Excellent video capabilities", "OliviaTaylor"));
            listOfProducts.Add(new Product("Fitbit Charge 3", "Tracks heart rate and activity", "AveryJones"));
            listOfProducts.Add(new Product("Apple Watch Series 4", "Great for notifications and apps", "JonathanKim"));
            listOfProducts.Add(new Product("Samsung Gear Sport", "Nice design and features", "AshleyNguyen"));
            listOfProducts.Add(new Product("Dell XPS 13", "Solid laptop for work", "MichaelRobinson"));
            listOfProducts.Add(new Product("MacBook Pro", "Excellent design and performance", "EmilyDavis"));
            listOfProducts.Add(new Product("Lenovo ThinkPad X1 Carbon", "Built like a tank", "DavidLee"));
            listOfProducts.Add(new Product("Canon EOS R", "Excellent camera for photography", "KatieClark"));
            listOfProducts.Add(new Product("Nikon D850", "Great for shooting in low light", "BrianSmith"));
            listOfProducts.Add(new Product("Sony a7 III", "Excellent video capabilities", "OliviaTaylor"));
            listOfProducts.Add(new Product("Fitbit Charge 3", "Tracks heart rate and activity", "AveryJones"));
            listOfProducts.Add(new Product("Apple Watch Series 4", "Great for notifications and apps", "JonathanKim"));
            listOfProducts.Add(new Product("Samsung Gear Sport", "Nice design and features", "AshleyNguyen"));
            listOfProducts.Add(new Product("AAAAAAAAPPP", "COOOOOOOOOOOOOOMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEENT", "UUUUUUUUUUUUUUUUUUSEEEEEER"));
            foreach (var item in listOfProducts)
            {
                DataGridView.Items.Add(item);
            }
        }

        private class Product
        {
            public Product(string name, string comment, string username)
            {
                Name = name;
                Comment = comment;
                Username = username;
            }

            public string Name { get; set; }
            public string Comment { get; set; }
            public string Username { get; set; }
        }
    }
}