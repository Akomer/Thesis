﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class StatisticInfo
    {
        [DataMember]
        public String CardPackName { get; set; }
        [DataMember]
        public List<Int32> StatInfo { get; set; }
    }
}
