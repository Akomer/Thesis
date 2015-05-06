using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    public class MainControlChangeEventArgs : EventArgs
    {
        private Type _newUserControl;
        private Type _newViewModel;
        public MainControlChangeEventArgs(Type uc, Type vm)
        {
            this._newUserControl = uc;
            this._newViewModel = vm;
        }

        public Type NewUserControl { get { return _newUserControl; } }
        public Type NewViewModel { get { return _newViewModel; } }
    }
}
