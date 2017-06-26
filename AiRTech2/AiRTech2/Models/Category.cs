using System.Collections.Generic;

namespace AiRTech2.Models
{
    public class Category : BaseDataObject
    {
        private string _description = string.Empty;
        private List<Subject> _subjects;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public List<Subject> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value);
        }
    }
}
