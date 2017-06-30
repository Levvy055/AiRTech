using System;
using System.Reflection;
using AiRTech2.Models;
using AiRTech2.Models.Subjects;
using AiRTech2.ViewModels.Subjects.BasicSignalParams;
using AiRTech2.Views.UniViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiRTech2.Views.Subjects.BasicSignalParams
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicSignalParamsPage : SubjectBasicPage
    {
        private BasicSignalParamsViewModel _viewModel;
        private readonly ContentView _dcView;
        private readonly ContentView _acView;
        private EnBasicSignalParams _currentView;

        public BasicSignalParamsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new BasicSignalParamsViewModel();
            _dcView = new SimpleImagesView(_viewModel,
                ImgPath+"bsp_dc1.png");
            _acView = new SimpleImagesView(_viewModel,
                ImgPath + "bsp_ac1.png");
            Title = _viewModel.Title;
        }

        public override void GoToView(Subject subject)
        {
            if (!Enum.IsDefined(typeof(EnBasicSignalParams), subject.En))
            {
                throw new ArgumentOutOfRangeException(nameof(subject),
                      "Value should be defined in the EnBasicSignalParams enum.");
            }
            _currentView = (EnBasicSignalParams)Convert.ChangeType(subject.En, typeof(EnBasicSignalParams));
            switch (_currentView)
            {
                case EnBasicSignalParams.Dc:
                    Content = _dcView;
                    break;
                case EnBasicSignalParams.Ac:
                    Content = _acView;
                    break;
            }
            _viewModel.Update(subject);
            UpdateChildrenLayout();
        }
    }
}