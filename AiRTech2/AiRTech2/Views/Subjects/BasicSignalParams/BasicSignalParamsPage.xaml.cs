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
        private EnBasicSignalParams _currentView = EnBasicSignalParams.NaN;

        public BasicSignalParamsPage()
        {
            BindingContext = _viewModel = new BasicSignalParamsViewModel();
            InitializeComponent();
            Title = _viewModel.Title;
            foreach (var v in _viewModel.Views)
            {
                ViewsContainer.Children.Add(v);
            }
        }

        public override void ChangeViewTo(Subject subject)
        {
            if (!Enum.IsDefined(typeof(EnBasicSignalParams), subject.En))
            {
                throw new ArgumentOutOfRangeException(nameof(subject),
                      "Value should be defined in the EnBasicSignalParams enum.");
            }
            var v = (EnBasicSignalParams)Convert.ChangeType(subject.En, typeof(EnBasicSignalParams));
            if (v != _currentView)
            {
                _currentView = v;
                _viewModel.Update(subject);
            }
        }
    }
}