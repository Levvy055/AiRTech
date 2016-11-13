using Xamarin.Forms;
using MvvmHelpers;

namespace AiRTech.Views.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        protected Page page;
        protected Command command;

        public ViewModelBase(Page page)
        {
            this.page = page;
        }
    }
}