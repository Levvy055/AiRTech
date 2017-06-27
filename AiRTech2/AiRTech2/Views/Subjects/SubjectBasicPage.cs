using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AiRTech2.Views.Subjects
{
    public abstract class SubjectBasicPage : ContentPage
    {
        public abstract void GoToView(Enum view);
    }
}
