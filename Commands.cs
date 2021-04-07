using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MasterLabWPF
{
    public class SaveListCommand : ICommand
    {
        public SaveListCommand()
        {

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            /*
            if (!(parameter is List<Product>))
                return false;
            else 
                return true;*/
            return true;
        }

        private void Serialize(string filename, Type type, object obj)
        {

            var serializer = new XmlSerializer(type);
            using (var stream = new StreamWriter(filename))
            {
                serializer.Serialize(stream, obj); //
            }

        }

        public void Execute(object parameter)
        {
            var productList = parameter as List<Product>;
            if (productList is null) throw new NullReferenceException();

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
    }
}
