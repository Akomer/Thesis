using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    public delegate void Event_mainControlChange(ViewModel.MainControlChangeEventArgs args);

    public class MainViewModelBase : ViewModelBase
    {
        public event Event_mainControlChange ChangeMainWindowContent;

        protected void OnChangeMainWindowContent(Type t_uc, Type t_vm)
        {
            if (this.ChangeMainWindowContent != null)
            {
                this.ChangeMainWindowContent(new ViewModel.MainControlChangeEventArgs(t_uc, t_vm));
            }
        }
    }
}
