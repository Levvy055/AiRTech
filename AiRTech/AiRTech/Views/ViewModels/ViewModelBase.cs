using System.ComponentModel;
using System.Reflection;
using AiRTech.Core.Subjects;
using Xamarin.Forms;
using MvvmHelpers;

namespace AiRTech.Views.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        public ViewModelBase(Page page)
        {
            Page = page;
            var t = page.GetType();
            var p = t.GetRuntimeProperty("Subject");
            if (p != null)
            {
                var s = p.GetValue(page) as Subject;
                Subject = s;
            }
        }

        public Page Page { get; }
        public Subject Subject { get; }
    }
}