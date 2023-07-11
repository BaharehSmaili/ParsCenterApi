using Common.Converters.jsonNetConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Models.Basic
{
    public class BaseSettableJsonSupport : BaseModel, ISettableJsonSupport
    {
        [JsonIgnore]
        public Dictionary<string, FieldDeserializationStatus> fieldStatus { get; set; } = new Dictionary<string, FieldDeserializationStatus>();

        public string getNotSetPropertiesAsString()
        {
            return string.Join(",", fieldStatus.Where(x => FieldDeserializationStatus.WasNotPresent.Equals(x.Value)
                    && !"fieldStatus".Equals(x.Key)).Select(x => x.Key).ToList());
        }
    }
}
