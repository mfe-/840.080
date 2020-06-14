using DDILibrary;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDIApp.ViewModels
{
    public class DrugPageViewModel : ViewModelBase
    {
        public DrugPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }
        public override void Initialize(INavigationParameters parameters)
        {
            if(parameters.ContainsKey(nameof(Drug)))
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
    }
}
