namespace AiRTech2.Models
{
    public class Category : BaseDataObject
    {
        string _text = string.Empty;
        string _description = string.Empty;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}
