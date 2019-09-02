using Challenge.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Challenge.Model
{
    [DataContract]
    public class Monitor
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public MonitorEventType Type { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "dateTime")]
        public DateTime DateTime { get; set; }

    }
}
