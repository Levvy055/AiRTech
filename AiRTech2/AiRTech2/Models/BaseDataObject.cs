using System;
using AiRTech2.Helpers;

namespace AiRTech2.Models
{
    public class BaseDataObject : ObservableObject
    {
        private string _title;
        private string _id;

        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
            _title = string.Empty;
        }

        /// <summary>
        /// Id for item
        /// </summary>
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
