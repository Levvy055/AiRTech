using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiRTech2.Helpers;
using AiRTech2.Models;
using AiRTech2.Models.Subjects;
using AiRTech2.Views.UniViews;
using Xamarin.Forms;

namespace AiRTech2.ViewModels.Subjects.BasicSignalParams
{
    public class BasicSignalParamsViewModel : SubjectViewModel
    {
        private readonly ContentView _dcView;
        private readonly ContentView _acView;
        private readonly ContentView _enView;
        private readonly ContentView _pView;
        private ObservableRangeCollection<View> _views;

        public BasicSignalParamsViewModel()
        {
            Title = BaseTitle = "Podstawowe parametry sygnału";
            _dcView = new SimpleImagesView(
                ImgPath + "bsp_dc1.png");
            _acView = new SimpleImagesView(
                ImgPath + "bsp_ac1.png");
            _enView = new SimpleImagesView(
                ImgPath + "bsp_e_sample1.png", ImgPath + "bsp_e_sample2.png");
            _pView = new SimpleImagesView(
                ImgPath + "bsp_pow1.png");
            Views = new ObservableRangeCollection<View>(new[] { _dcView, _acView, _enView, _pView });
        }

        public override void Update(Subject subject)
        {
            base.Update(subject);
            foreach (var view in Views)
            {
                view.IsVisible = false;
            }
            switch (subject.En)
            {
                case EnBasicSignalParams.Dc:
                    _dcView.IsVisible = true;
                    break;
                case EnBasicSignalParams.Ac:
                    _acView.IsVisible = true;
                    break;
                case EnBasicSignalParams.EnergyOfSample:
                    _enView.IsVisible = true;
                    break;
                case EnBasicSignalParams.Power:
                    _pView.IsVisible = true;
                    break;
            }
        }

        public ObservableRangeCollection<View> Views
        {
            get => _views;
            set => SetProperty(ref _views, value);
        }
    }
}
