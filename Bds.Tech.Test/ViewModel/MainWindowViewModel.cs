using Bds.Tech.Test.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        IEnumerable<Result> _results;
        ResultMixer mixer;
        public IEnumerable<Result> Results 
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }        

        private RelayCommand<string> _searchCommand = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<string> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand<string>(Search, CanSearch));
            }
        }

        private bool CanSearch(string searchTerm)
        {
            return !string.IsNullOrEmpty(searchTerm);
        }
        private async void Search(string searchTerm)
        {
            Results = await mixer.GetCombinedSearchResultsAsync(searchTerm);
        }

        private RelayCommand _addResultCommand = null;

        public RelayCommand AddResultCommand
        {
            get
            {
                return _addResultCommand ?? (_addResultCommand = new RelayCommand(AddToInvestigation, CanAddToInvestigation));
            }
        }

        private bool CanAddToInvestigation()
        {
            return Results.Any(i => i.IsSelected);
        }

        List<Result> temp = new List<Result>();
        private void AddToInvestigation()
        {
            var selected = Results.Where(i => i.IsSelected);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RegisterSearchEngines()
        {
            mixer = new ResultMixer();
            mixer.RegisterSearchEngine(new GoogleSearchEngine());
            mixer.RegisterSearchEngine(new BingSearchEngine());
        }

        public MainWindowViewModel()
        {
            Results = new ObservableCollection<Result>();
            RegisterSearchEngines();
        }
    }
}
