using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    public delegate void MyEventDelegate(ViewModel.ViewModelChangeEventArgs args);

    public class MainViewModelBase : ViewModelBase
    {
        public event MyEventDelegate ChangeMainWindowContent;

        protected void OnChangeMainWindowContent(Type T)
        {
            if (this.ChangeMainWindowContent != null)
            {
                this.ChangeMainWindowContent(new ViewModel.ViewModelChangeEventArgs(T));
            }
        }
    }
}
