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

namespace MasterLabWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Product> productList;
        public MainWindow()
        {
            productList = new List<Product>();

            
            
            InitializeComponent();

            ProductGrid.ItemsSource = productList;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product(
                ShortNameTB.Text,
                FullNameTB.Text,
                DescriptionTB.Name, //fix
                CategoryLB.Text,
                (int)RatingUDC.Value,
                (int)PriceUDC.Value, 
                null
                );
            productList.Add(product);

            ProductGrid.Items.Refresh();
        }
    }
}
