using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.Models;

namespace AiRTech2.ViewModels.Subjects
{
    public abstract class SubjectViewModel : BaseViewModel
    {
        public static string ImgPath = "AiRTech2.Resources.Images.Subjects.";

        public virtual void Update(Subject subject)
        {
            Title = subject.Title;
        }
        
        protected string BaseTitle { get; set; }
    }
}
