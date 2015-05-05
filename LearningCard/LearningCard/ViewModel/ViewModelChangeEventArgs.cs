using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    public class ViewModelChangeEventArgs : EventArgs
    {
        private Type newWindow;
        public ViewModelChangeEventArgs(Type t)
        {
            this.newWindow = t;
        }

        public Type Data { get { return newWindow; } }
    }
}
