using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech.Core.Subjects.Def;
using Xamarin.Forms;

namespace AiRTech.Views.ViewModels
{
    public class DefinitionsViewModel : ViewModelBase
    {
        public DefinitionsViewModel(DefinitionsPage page) : base(page)
        {
            Title = page.Subject.Name + " - Definicje";
        }

        public List<Definition> Definitions => Subject.Base.Definitions;
    }
}
