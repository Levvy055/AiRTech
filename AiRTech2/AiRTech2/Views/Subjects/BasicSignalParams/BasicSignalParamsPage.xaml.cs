using System;
using System.Reflection;
using AiRTech2.Models.Subjects;
using AiRTech2.ViewModels.Subjects.BasicSignalParams;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiRTech2.Views.Subjects.BasicSignalParams
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicSignalParamsPage : SubjectBasicPage
    {
        private BasicSignalParamsViewModel _viewModel;
        private readonly ContentView _dcView = new DcParamView();
        private EnBasicSignalParams _currentView;

        public BasicSignalParamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new BasicSignalParamsViewModel();
            Title = _viewModel.Title;
        }

        public override void GoToView(Enum view)
        {
            if (!Enum.IsDefined(typeof(EnBasicSignalParams), view))
            {
                throw new ArgumentOutOfRangeException(nameof(view),
                      "Value should be defined in the EnBasicSignalParams enum.");
            }
            _currentView = (EnBasicSignalParams)Convert.ChangeType(view, typeof(EnBasicSignalParams));
            switch (_currentView)
            {
                case EnBasicSignalParams.Dc:
                    Content = _dcView;
                    break;
                case EnBasicSignalParams.Ac:

                    break;
            }
            UpdateChildrenLayout();
        }
    }
}