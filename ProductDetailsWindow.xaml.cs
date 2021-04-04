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
using System.Windows.Shapes;

namespace MasterLabWPF
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        EventHandler ProductChangedEvent;
        Product SelectedProduct;
        string selectedImg;
        public ProductDetailsWindow(in Product selectedProduct)
        {
            InitializeComponent();
            SelectedProduct = selectedProduct;
            ShortNameTB.Text = SelectedProduct.ShortName;
            FullNameTB.Text = SelectedProduct.FullName;
            CategoryLB.Text = SelectedProduct.Category;
            RatingUDC.Value = SelectedProduct.Rating;
            DiscountUDC.Value = SelectedProduct.Discount;
            PriceUDC.Value = SelectedProduct.Price;
            AmountUDC.Value = SelectedProduct.Amount;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedProduct.ShortName = ShortNameTB.Text;
            SelectedProduct.FullName = FullNameTB.Text;
            SelectedProduct.Category = CategoryLB.Text;
            SelectedProduct.Rating = (double)RatingUDC.Value;
            SelectedProduct.Discount = (int)DiscountUDC.Value;
            SelectedProduct.Price = (int)PriceUDC.Value;
            SelectedProduct.Amount = (int)AmountUDC.Value;
            SelectedProduct.Imagesrc = selectedImg;
            Close();
            //ProductChangedEvent((object)this, null);


        }
    }
}
