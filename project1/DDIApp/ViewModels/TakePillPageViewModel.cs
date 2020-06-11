using System;
using System.Collections.Generic;
using System.Text;
using Prism.Navigation;

namespace DDIApp.ViewModels
{
    public class TakePillPageViewModel : ViewModelBase
    {
        public TakePillPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Take Pill";
        }
    }
}
