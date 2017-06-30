using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AiRTech2.Models;
using AiRTech2.Models.Subjects;
using AiRTech2.Views.Subjects.BasicSignalParams;
using Xamarin.Forms;

namespace AiRTech2.Services
{
    public class DataStore
    {
        private bool _isInitialized;
        private List<Category> _categories;
        
        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool forceRefresh = false)
        {
            Initialize();
            return await Task.FromResult(_categories);
        }

        public void Initialize()
        {
            if (_isInitialized)
            { return; }

            _categories = new List<Category>
            {
                new Category { Title = "Podstawowe parametry sygnałów", Page = new BasicSignalParamsPage(), Subjects = new List<Subject>
                {
                    new Subject{Title = "Składowa stała", En = EnBasicSignalParams.Dc},
                    new Subject{Title = "Składowa przemienna", En = EnBasicSignalParams.Ac},
                    new Subject{Title = "Energia sygnału | próbki", En = EnBasicSignalParams.EnergyOfSample},
                    new Subject{Title = "Moc sygnału", En = EnBasicSignalParams.Power},
                }},
                new Category { Title = "Szeregi i transformacje Fouriera"},
                new Category { Title = "DFT i FFT"},
                new Category { Title = "Próbkowanie sygnałów"},
                new Category { Title = "Kwantowanie sygnałów"},
                new Category { Title = "Binarne stałoprzecinkowe reprezentacje próbek sygnałów", Description=""},
                new Category { Title = "Binarne ułamkowe reprezentacje próbek sygnałów", Description=""},
                new Category { Title = "Przekształcenie Z", Description=""},
                new Category { Title = "Liniowe stacjonarne układy dyskretne", Description=""},
                new Category { Title = "Splot sygnałów", Description=""},
                new Category { Title = "Filtry cyfrowe", Description=""},
                new Category { Title = "Projektowanie filtrów cyfrowych", Description=""},
                new Category { Title = "Filtry pasywne (bezstratne)", Description=""},
                new Category { Title = "Teoria aproksymacji", Description=""},
                new Category { Title = "Układy dynamiczne", Description=""},
                new Category { Title = "Sprzężenie zwrotne i stabilność układów", Description=""},
                new Category { Title = "Wielomiany Hurwitza i kryteria stabilności", Description=""},
                new Category { Title = "Filtry reaktancyjne", Description=""},
                new Category { Title = "Cyfrowe filtry falowe (WDFs)", Description=""},
                new Category { Title = "Sygnały losowe i pojęcie informacji", Description=""},
                new Category { Title = "Filtracja sygnałów losowych", Description=""},
                new Category { Title = "Elementy teorii informacji i kodowanie danych", Description=""},
                new Category { Title = "Dwuwymiarowe filtry cyfrowe", Description=""},
                new Category { Title = "Transformacja zafalowaniowa", Description=""},
                new Category { Title = "Ludzki system wizyjny", Description=""},
                new Category { Title = "Proces słyszenia i przetwarzanie sygnałów audio", Description=""},
                new Category { Title = "Techniki telewizyjne i multimedialne", Description=""},
                new Category { Title = "Sieci neuronowe", Description=""},
            };

            _isInitialized = true;
        }
    }
}
