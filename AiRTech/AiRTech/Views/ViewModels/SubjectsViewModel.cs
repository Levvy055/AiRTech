using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AiRTech.Core.Commands;
using AiRTech.Core.Subjects;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class SubjectsViewModel :ViewModelBase
    {
        public SubjectsViewModel(Page page) : base(page)
        {
            Title = "Przedmioty";
        }


    }
}
