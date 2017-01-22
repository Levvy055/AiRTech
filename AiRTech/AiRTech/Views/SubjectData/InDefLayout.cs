using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AiRTech.Views.SubjectData
{
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InDefLayout
    {
        TextUnderImage,
        TextOverImage,
        List,
        HeaderAndText
    }
}
