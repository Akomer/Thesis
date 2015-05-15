using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace LearningCard.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase() { }

        public Dictionary<String, String> ActiveLanguage
        {
            get
            {
                return Model.GlobalLanguage.Instance.GetDict();
            }
            set { }
        }

        virtual public void RefreshLanguage()
        {
            this.OnPropertyChanged("ActiveLanguage");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
