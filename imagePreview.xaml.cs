using System; 
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для imagePreview.xaml
    /// </summary>
    public partial class imagePreview : UserControl, INotifyPropertyChanged
    {
        private string source;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Source {
            get => source;
                
            set
            {
                source = value; 
                if(PropertyChanged!=null)
                 PropertyChanged(this, new PropertyChangedEventArgs("Source"));
            }
        }
        public imagePreview()
        {

            //Source = @"C:\4sem\OOP\Labs\MasterLabWPF\bin\Debug\MultimediaSrc\toad.jpg";
            InitializeComponent();
            //img.Source = new BitmapImage(new Uri(Source));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!(Source is null))
            { 
                var wnd = new imageFull(Source);
                wnd.Show();
            }
        }
    }
}
