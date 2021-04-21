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
        public string ImageSource { get; set; }
        public static RoutedUICommand command { get; set; }
        public imageFull(string imageSource)
        {
            ImageSource = imageSource;
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.Escape));
            command = new RoutedUICommand("Exit via dialogue window", "Exit", typeof(imageFull), inputs);

            InitializeComponent();

            img.Source = new BitmapImage(new Uri(imageSource));

            this.Width = img.Source.Width;
            this.Height = img.Source.Height;

        }
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
          
            var box = new boolDialogue("Закрыть?");
            box.ShowDialog();
            if (box.DialogResult.GetValueOrDefault())
                this.Close();
        }
    }
}

