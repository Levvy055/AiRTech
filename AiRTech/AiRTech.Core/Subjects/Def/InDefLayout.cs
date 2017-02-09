﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AiRTech.Core.Subjects.Def
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InDefLayout
    {
        TextUnderImage,
        TextOverImage,
        List,
        HeaderAndText,
        OList
    }
}
