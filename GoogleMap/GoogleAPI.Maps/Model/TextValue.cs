using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GoogleAPI.Maps.Model {
    [DataContract]
    public class TextValue {
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}
