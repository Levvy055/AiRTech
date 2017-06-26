namespace AiRTech2.Models
{
    public class Category : BaseDataObject
    {
        string _description = string.Empty;
        
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}
