using Xamarin.Forms;
using MvvmHelpers;

namespace AiRTech.Views.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        protected Page page;

        public ViewModelBase(Page page)
        {
            this.page = page;
        }
    }
}