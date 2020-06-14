using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DDILibrary;
using Prism.Commands;
using Prism.Navigation;

namespace DDIApp.ViewModels
{
    public class TakePillPageViewModel : ViewModelBase
    {
        private readonly DrugService _drugService;

        public TakePillPageViewModel(
            INavigationService navigationService,
            DrugService drugService)
            : base(navigationService)
        {
            Title = "Take Pill";
            _drugService = drugService;
            DrugList = _drugService.Drugs;
            TakePillCommand = new DelegateCommand<Drug>(OnTakePillCommand);
        }

        private IEnumerable<Drug> _DrugList;
        public IEnumerable<Drug> DrugList
        {
            get { return _DrugList; }
            set { SetProperty(ref _DrugList, value, nameof(DrugList)); }
        }

        private Drug _SelectedDrug;
        public Drug SelectedDrug
        {
            get { return _SelectedDrug; }
            set { SetProperty(ref _SelectedDrug, value, nameof(SelectedDrug)); }
        }

        public ICommand TakePillCommand { get; }

        protected void OnTakePillCommand(Drug drug)
        {
            if (drug != null)
            {
                drug.Taken.Add(DateTime.Now);
                _drugService.SaveDrugs();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                NavigationService.NavigateAsync(nameof(MainPageViewModel));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            }
        }
    }
}
