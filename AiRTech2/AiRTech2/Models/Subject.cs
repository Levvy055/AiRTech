using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.Models.Subjects;
using Xamarin.Forms;

namespace AiRTech2.Models
{
    public class Subject : BaseDataObject
    {
        public ContentView View { get; set; }
        public Enum En { get; set; }
    }
}
