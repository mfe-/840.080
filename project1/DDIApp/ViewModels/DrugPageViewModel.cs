using DDILibrary;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DDIApp.ViewModels
{
    public class DrugPageViewModel : ViewModelBase
    {
        private readonly DrugService _drugService;
        public DrugPageViewModel(
            INavigationService navigationService,
            DrugService drugService)
            : base(navigationService)
        {
            _drugService = drugService;
            RemoveCommand = new DelegateCommand<Drug>(OnRemoveCommand);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(Drug)))
            {
                Drug = parameters[nameof(Drug)] as Drug;
            }
        }

        private Drug _Drug;
        public Drug Drug
        {
            get { return _Drug; }
            set { SetProperty(ref _Drug, value, nameof(Drug)); }
        }

        public ICommand RemoveCommand { get; }

        protected void OnRemoveCommand(Drug drug)
        {
            if (drug != null)
            {
                _drugService.Drugs.Remove(drug);
                _drugService.SaveDrugs();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                NavigationService.NavigateAsync(nameof(MainPageViewModel));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            }
        }
    }
}
