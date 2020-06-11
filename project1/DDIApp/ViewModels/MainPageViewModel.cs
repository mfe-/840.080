using DDIApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DDIApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly DrugService _drugService;
        public MainPageViewModel(INavigationService navigationService, DrugService drugService) : base(navigationService)
        {
            Title = "Medicine Overview";
            NavigateToCommand = new DelegateCommand<string>(OnNavigationToCommand);
            NavigateToDrugCommand = new DelegateCommand<Drug>(OnNavigationToCommand);
            _drugService = drugService;
            _Drugs = new List<Drug>();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Drugs = _drugService.Drugs;
        }


        private IList<Drug> _Drugs;
        public IList<Drug> Drugs
        {
            get { return _Drugs; }
            set { SetProperty(ref _Drugs, value, nameof(Drugs)); }
        }

        public ICommand NavigateToCommand { get; }

        protected void OnNavigationToCommand(string viewModelToNavigate)
        {
            NavigationService.NavigateAsync(viewModelToNavigate);
        }

        public ICommand NavigateToDrugCommand { get; }

        protected void OnNavigationToCommand(Drug drug)
        {
            NavigationService.NavigateAsync(nameof(DrugPageViewModel), new NavigationParameters() { { nameof(Drug), drug } });
        }


    }
}
