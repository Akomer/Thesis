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
        private object[] _args;

        public MainControlChangeEventArgs(Type uc, Type vm, object[] args = null)
        {
            this._newUserControl = uc;
            this._newViewModel = vm;
            this._args = args;
        }

        public Type NewUserControl { get { return this._newUserControl; } }
        public Type NewViewModel { get { return this._newViewModel; } }
        public object[] args { get { return this._args; } }
    }
}
