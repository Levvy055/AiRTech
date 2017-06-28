using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.ViewModels.Subjects.BasicSignalParams;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiRTech2.Views.Subjects.BasicSignalParams
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DcParamView : ContentView
    {
        private readonly BasicSignalParamsViewModel _viewModel;

        public DcParamView(BasicSignalParamsViewModel viewModel)
        {
            BindingContext = _viewModel = viewModel;
            InitializeComponent();
        }
    }
}