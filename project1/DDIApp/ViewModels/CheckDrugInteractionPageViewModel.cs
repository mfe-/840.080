using Prism.Navigation;

namespace DDIApp.ViewModels
{
    public class CheckDrugInteractionPageViewModel : ViewModelBase
    {
        public CheckDrugInteractionPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Check Drug Interaction";
        }
    }
}
