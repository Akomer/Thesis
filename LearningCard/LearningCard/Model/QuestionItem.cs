using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    class QuestionItem
    {
        private object _Data;
        public object Data 
        { 
            get { return this._Data; }
            set  { this._Data = value; }
        }
        public Type DataType { get; set; }
    }
}
