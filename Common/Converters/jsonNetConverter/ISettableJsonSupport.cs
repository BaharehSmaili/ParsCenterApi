using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Converters.jsonNetConverter
{
    public interface ISettableJsonSupport
    {
        Dictionary<string, FieldDeserializationStatus> fieldStatus { get; set; }
        string getNotSetPropertiesAsString();
    }
}
