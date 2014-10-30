using System;
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

        private string _key = String.Empty;
        public string Key
        {
            get
            {
                if (String.IsNullOrEmpty(_key))
                    _key = String.Format("{0}_{1}", Type, ChildId);

                return _key;
            }
        }


        [DataMember]
        public string Type { get; private set; }
    }
}
