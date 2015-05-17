using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class InputTextDiagViewModel: MainViewModelBase
    {
        public String TextLabel { get; set; }
        public String InputText { get; set; }
        public DelegateCommand Command_DialogLoad { get; set; }
        public DelegateCommand Command_DialogCancel { get; set; }

        public InputTextDiagViewModel(View.SimpleTextInputDialog newDialog, String txtLabel)
        {
            this.TextLabel = txtLabel;
            this.Command_DialogLoad = new DelegateCommand(x => this.Execute_DialogLoad(newDialog));
            this.Command_DialogCancel = new DelegateCommand(x => this.Execute_DialogCancel(newDialog));
            this.InputText = "";
        }

        private void Execute_DialogLoad(View.SimpleTextInputDialog dialog)
        {
            dialog.DialogResult = true;
        }

        private void Execute_DialogCancel(View.SimpleTextInputDialog dialog)
        {
            dialog.DialogResult = false;
        }
    }
}
