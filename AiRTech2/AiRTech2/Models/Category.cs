using System.Collections.Generic;
using AiRTech2.Views.Subjects;
using Xamarin.Forms;

namespace AiRTech2.Models
{
    public class Category : BaseDataObject
    {
        private string _description = string.Empty;
        private List<Subject> _subjects;
        private SubjectBasicPage _page;
        private string _quantity = "Soon";

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public List<Subject> Subjects
        {
            get => _subjects;
            set
            {
                if (value != null && value.Count > 0)
                {
                    Quantity = value.Count.ToString();
                }
                else
                {
                    Quantity = "Soon";
                }
                SetProperty(ref _subjects, value);
            }
        }
        public SubjectBasicPage Page
        {
            get => _page;
            set => SetProperty(ref _page, value);
        }
        public string Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}
