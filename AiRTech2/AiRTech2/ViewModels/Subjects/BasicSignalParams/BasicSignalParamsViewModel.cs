using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.Helpers;
using AiRTech2.Models;
using Xamarin.Forms;

namespace AiRTech2.ViewModels.Subjects.BasicSignalParams
{
    public class BasicSignalParamsViewModel : SubjectViewModel
    {
        public BasicSignalParamsViewModel()
        {
            Title = BaseTitle = "Podstawowe parametry sygnału";
        }

        public override void Update(Subject subject)
        {
            base.Update(subject);

        }

        public override SubjectViewModel Clone()
        {
            return new BasicSignalParamsViewModel
            {

            };
        }
    }
}
