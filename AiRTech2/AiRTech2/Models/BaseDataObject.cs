using System;
using AiRTech2.Helpers;

namespace AiRTech2.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>
        public string Id { get; set; }
    }
}
