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
    public class BasicSignalParamsViewModel:BaseViewModel
    {
        public BasicSignalParamsViewModel()
        {
            Title = "Podstawowe parametry sygnału";
            Items=new ObservableRangeCollection<ContentView>();
        }

        public ObservableRangeCollection<ContentView> Items { get; }
    }
}
