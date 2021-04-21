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
        Stack<List<Product>> memento;
        Stack<List<Product>> mementoRedo;
        public ICommand saveListCommand { get; set; }
        List<Product> productList;
        string selectedImg;
        public MainWindow()
        {
            productList = new List<Product>();
            saveListCommand = new SaveListCommand();


            InitializeComponent();
           
            ProductGrid.ItemsSource = productList;
            FilterDropDown.SelectedIndex = 0;
            

            memento = new Stack<List<Product>>();
            mementoRedo = new Stack<List<Product>>();
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ProductGrid_SourceUpdated(null, null);


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
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
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
                ImgPreview.Source = selectedImg;
            }
            else
            {
                selectedImg = null;
                SelectedImageTextBlock.Text = "";

            }
           
        }

        private void LoadListButton_Click(object sender, RoutedEventArgs e)
        {
            var count0 = ProductGrid.Items.Count;
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML files (*.xml)|*.xml";
            dlg.FileName = "save.xml";
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + @"\Bin\MultemediaSrc\";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    ProductGrid_SourceUpdated(null, null);

                    productList.Clear();
                    productList = (List<Product>)Deserialize(dlg.FileName, typeof(List<Product>));

                    ProductGrid.ItemsSource = productList;
                    ProductGrid.Items.Refresh();

                }
                catch (Exception excep)
                {

                }
                System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            }
            else return;

            var count = ProductGrid.Items.Count;
        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
               

            
        }

        public void Serialize(string filename, Type type, object obj)
        {

            var serializer = new XmlSerializer(type);
            using (var stream = new StreamWriter(filename))
            {
                serializer.Serialize(stream, obj); //
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
            ProductGrid_SourceUpdated(null, null);

            productList.Clear();
            ProductGrid.ItemsSource = productList;
            ProductGrid.Items.Refresh();

            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            var index = row.GetIndex();
            if (index >= 0 && productList.Count > 0)
            {
                var product = row.Item as Product;
                var productDetailsWindow = new ProductDetailsWindow(in product);
                productDetailsWindow.ShowDialog();
                productList[index] = product;
                ProductGrid.ItemsSource = productList;
                
            }
            //var ProductDetailsWnd = new ProductDetailsWindow()
        }

        private void UpdateListButton_Click(object sender, RoutedEventArgs e)
        {
            ProductGrid_SourceUpdated(null, null);

            var filter = FilterDropDown.Text;
            if (filter == "Все")
            {
                ProductGrid.ItemsSource = productList;
            }
            else
            {
                var TempList = new List<Product>();
                foreach(var product in productList)
                {
                    if (product.Category == filter)
                        TempList.Add(product);
                }
                ProductGrid.ItemsSource = TempList;
            }
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();


            ProductGrid.Items.Refresh();

        }
        private void ChangeLang_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            switch (LanguageLB.Text)
            {
                case "Русский":
                    this.Resources = new ResourceDictionary()
                    {
                        Source = new Uri(@"C:\4sem\OOP\Labs\MasterLabWPF\Localization\RussianLang.xaml") // опять перестало работать
                    };
                   
                    break;
                case "English":

                    this.Resources = new ResourceDictionary()
                    {
                        Source = new Uri(@"Localization\EnglishLang.xaml")
                    };
                    
                    break;

            }

            
        }

        private void ChangeStyle_Click(object sender, RoutedEventArgs e)
        {
            switch(styleLB.Text)
            {
                case "Зелёный":
                    this.Resources = new ResourceDictionary()
                    {
                       // Source = new Uri(@"Themes\GreenStyle.xaml") круто было бы если бы работало ага да
                        Source = new Uri(@"C:\4sem\OOP\Labs\MasterLabWPF\Themes\GreenStyle.xaml")
                    }; 
                    break;
                case "Синий":
                    this.Resources = new ResourceDictionary()
                    {
                        Source = new Uri(@"C:\4sem\OOP\Labs\MasterLabWPF\Themes\BlueStyle.xaml")
                    }; 
                    break;
            }
        }

        private void ProductGrid_SourceUpdated(object sender, DataTransferEventArgs e) // умоляю, работай (Не работает)
        {
            memento.Push(productList.ToList()); // Костыли к нам приходят
        }

        private void redoButton_Click(object sender, RoutedEventArgs e)
        {
            if (mementoRedo.Count>0)
            {
                memento.Push(productList.ToList());
                productList = mementoRedo.Pop();

                ProductGrid.ItemsSource = productList;
                ProductGrid.Items.Refresh();
            }
           
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            if (memento.Count > 0)
            {
                mementoRedo.Push(productList);
                productList = memento.Pop();

                ProductGrid.ItemsSource = productList;
                ProductGrid.Items.Refresh();
            }
        }

        private void ProductGrid_CollectionUpdated(object sender, RoutedEventArgs e)
        {
            memento.Push(productList);
        }

        private void TabItem_Click(object sender, RoutedEventArgs e)
        {
            additemTab.Background = Brushes.BlueViolet;
              
        }
    }
}
