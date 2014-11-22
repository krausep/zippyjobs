using System;
using System.Runtime.Serialization;


namespace ZippyJobs.Models
{
    [Serializable]
    [DataContract]
    public class Job
    {
        public Job()
        {
            Type = "job";
        }

        [DataMember]
        public int JobId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int PointValue { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Type { get; private set; }

        public string Key
        {
            get
            {
                return String.Format("{0}_{1}", Type, JobId);
            }
        }
    }
}
