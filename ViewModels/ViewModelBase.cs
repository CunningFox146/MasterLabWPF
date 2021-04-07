using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MasterLabWPF
{
    class ViewModelBase
    {
        public ICommand saveListCommand { get; set; }
        public ViewModelBase()
        {
            this.saveListCommand = new SaveListCommand();
        }
    }
}
