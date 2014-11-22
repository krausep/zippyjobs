using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZippyJobs.Models
{
    [Serializable]
    [DataContract]
    public class Child
    {
        public Child()
        {
            Type = "child";
        }



        [DataMember]
        public int ChildId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime Birthday { get; set; }

        [DataMember]
        public List<int> Jobs { get; set; }

        [DataMember]
        public string Url { get; set; }

        public string Key
        {
            get
            {
                return String.Format("{0}_{1}", Type, ChildId);
            }
        }


        [DataMember]
        public string Type { get; private set; }
    }
}
