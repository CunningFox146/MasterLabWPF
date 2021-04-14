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
    /// Логика взаимодействия для imageFull.xaml
    /// </summary>
   
    public partial class imageFull : Window
    {
        //string imageSource;
        public imageFull(string imageSource)
        {
            InitializeComponent();
            img.Source = new BitmapImage(new Uri(imageSource));

            this.Width = img.Source.Width;
            this.Height = img.Source.Height;

        }
    }
}
