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

        public static readonly DependencyProperty SourceProperty;
        static FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
        static imagePreview()
        {
            
            metadata.CoerceValueCallback = coerceValueCallback;
            metadata.DefaultValue = System.IO.Directory.GetCurrentDirectory() + @"\MultimediaSrc\brokenimg.png";
            SourceProperty = DependencyProperty.Register(nameof(Source), typeof(string), typeof(imagePreview), metadata, validateValueCallback);


        }

        private static object coerceValueCallback(DependencyObject d, object baseValue)
        {
            if ((string)baseValue == "" || baseValue is null)
                return System.IO.Directory.GetCurrentDirectory() + @"\MultimediaSrc\brokenimg.png";
            return baseValue;
        }

        private static bool validateValueCallback(object value)
        {
            if ((string)value == "" || value is null)
                return false;
            return true;
        }

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
       "PreviewClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(imagePreview));
        public event RoutedEventHandler PreviewClick
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }
        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(imagePreview.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        //рудимент        
        public string Source2 {
            get => source;
                
            set
            {
                source = value; 
                if(PropertyChanged!=null)
                 PropertyChanged(this, new PropertyChangedEventArgs("Source2"));
            }
        }
        public imagePreview()
        {
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickEvent();
            if (!(Source is null))
            { 
                var wnd = new imageFull((string)Source);
                wnd.Show();
            }
        }
    }
}
