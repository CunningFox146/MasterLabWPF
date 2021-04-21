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
    /// Логика взаимодействия для boolDialogue.xaml
    /// </summary>

  
    public partial class boolDialogue : Window
    {
        public string Message { get; set; }
        public boolDialogue(string message)
        {
            this.Message = message;
            InitializeComponent();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ДаButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
