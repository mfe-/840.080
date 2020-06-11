using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DDIApp.Models;
using Prism.Commands;
using Prism.Navigation;

namespace DDIApp.ViewModels
{
    public class AddMedicinePageViewModel : ViewModelBase
    {
        public AddMedicinePageViewModel(INavigationService navigationService, DrugService drugService) : base(navigationService)
        {
            Title = "Add Medicine";
            Drug = new Drug();
            DrugService = drugService;
            AddMedicineCommand = new DelegateCommand(OnAddMedicineCommand);
        }

        private Drug _Drug;
        public Drug Drug
        {
            get { return _Drug; }
            set { SetProperty(ref _Drug, value, nameof(Drug)); }
        }

        private DrugService DrugService { get; }
        public ICommand AddMedicineCommand { get; }

        protected void OnAddMedicineCommand()
        {
            DrugService.AddDrug(Drug);
            NavigationService.NavigateAsync(nameof(MainPageViewModel));
        }
    }
}
