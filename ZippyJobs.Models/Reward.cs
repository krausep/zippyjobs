using System;
using System.Runtime.Serialization;


namespace ZippyJobs.Models
{
    [Serializable]
    [DataContract]
    public class Reward
    {
        public Reward()
        {
            Type = "reward";
        }

        [DataMember]
        public int RewardId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int PointValue { get; set; }

        [DataMember]
        public RedeemableInterval RedeemableInterval { get; set; }

        [DataMember]
        public string Type { get; private set; }

        private string _key = String.Empty;
        public string Key
        {
            get
            {
                if (String.IsNullOrEmpty(_key))
                    _key = String.Format("{0}_{1}", Type, RewardId);

                return _key;
            }
        }
    }

    [Serializable]
    [DataContract]
    public enum RedeemableInterval
    {
        Unlimited,
        OnceDaily,
        TwiceDaily,
        OnceWeekly
    }
}
