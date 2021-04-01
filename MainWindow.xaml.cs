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
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;

namespace MasterLabWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Product> productList;
        string selectedImg;
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
                CategoryLB.Text,
                (double)RatingUDC.Value,
                (int)PriceUDC.Value,
                selectedImg,
                //(!(selectedImg is null)) ? selectedImg: null, // Бесполезность этого мува компенсируется моим страхом перед nullreferenceexception
                (int)DiscountUDC.Value,
                (int)AmountUDC.Value
                );
            productList.Add(product);

            ProductGrid.Items.Refresh();
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + @"\Bin\MultemediaSrc\";
            if (dlg.ShowDialog() == true)
            {
                selectedImg = dlg.FileName;
                SelectedImageTextBlock.Text = System.IO.Path.GetFileName(dlg.FileName);
            }
            else
            {
                selectedImg = null;
                SelectedImageTextBlock.Text = "";

            }
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.FileName = "save.xml";
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + @"\Bin\MultemediaSrc\";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    productList.Clear();
                    productList = (List<Product>)Deserialize(dlg.FileName, typeof(List<Product>));

                    ProductGrid.ItemsSource = productList;
                    ProductGrid.Items.Refresh();
                }
                catch (Exception excep)
                {

                }
            }
            else return;
        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.FileName = "save.xml";
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + @"\Bin\MultemediaSrc\";

            if (dlg.ShowDialog() == true)
            {

                try
                {
                    Serialize(dlg.FileName, typeof(List<Product>), productList);

                }
                catch (Exception excep)
                {
                    
                }

            }

               

            
        }

        public void Serialize(string filename, Type type, object obj)
        {

            var serializer = new XmlSerializer(type);
            using (var stream = new StreamWriter(filename))
            {
                serializer.Serialize(stream, obj);
            }

        }

        public object Deserialize(string filename, Type type)
        {

            var serializer = new XmlSerializer(type);
            using (var stream = new StreamReader(filename))
            {
                return serializer.Deserialize(stream);
            }

        }

        private void ClearListButton_Click(object sender, RoutedEventArgs e)
        {
            productList.Clear();
            ProductGrid.ItemsSource = productList;
            ProductGrid.Items.Refresh();
        }
    }
}
