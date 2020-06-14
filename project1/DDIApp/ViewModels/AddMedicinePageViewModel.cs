using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DDILibrary;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;

namespace DDIApp.ViewModels
{
    public class AddMedicinePageViewModel : ViewModelBase
    {
        private readonly DrugInteractionService _drugInteractionService;
        private readonly IDialogService _dialogService;

        public AddMedicinePageViewModel(
            INavigationService navigationService,
            DrugService drugService,
            DrugInteractionService drugInteractionService,
            IDialogService dialogService)
            : base(navigationService)
        {
            _drugInteractionService = drugInteractionService;
            _dialogService = dialogService;
            Title = "Add Medicine";
            DrugService = drugService;
            AddMedicineCommand = new DelegateCommand(OnAddMedicineCommand);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            RefreshDrugSuggestionsTask = Task.Run(RefreshDrugSuggestions);
        }
        /// <summary>
        /// When navigating away from this viewmodel
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            RunRefreshDrugSuggestions = false;
            StopRefreshDrugSuggestion();

        }

        private void StopRefreshDrugSuggestion()
        {
            RunRefreshDrugSuggestions = false;
            if (RefreshDrugSuggestionsTask != null)
            {
                while (!RefreshDrugSuggestionsTask.IsCompleted)
                {
                    //wait until task operation is finished
                }
            }
        }

        /// <summary>
        /// The entered drug name
        /// </summary>
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
                RunRefreshDrugSuggestions = true;
            }
        }

        private bool _RunRefreshDrugSuggestions;
        public bool RunRefreshDrugSuggestions
        {
            get { return _RunRefreshDrugSuggestions; }
            set { SetProperty(ref _RunRefreshDrugSuggestions, value, nameof(RunRefreshDrugSuggestions)); }
        }
        private Task RefreshDrugSuggestionsTask;
        /// <summary>
        /// Fetches drug suggessions depending on the enterted name
        /// </summary>
        /// <returns></returns>
        public async Task RefreshDrugSuggestions()
        {
            string lastName = Name;
            RunRefreshDrugSuggestions = true;
            while (RunRefreshDrugSuggestions)
            {
                if (!RunRefreshDrugSuggestions)
                {
                    return;
                }
                if (!String.IsNullOrEmpty(_Name) && _Name.Length > 1 && Name != lastName)
                {
                    lastName = Name;
                    DrugSuggestions = _drugInteractionService.SuggestDrugs(lastName);
                    IsStepTwoVisible = DrugSuggestions.Any();
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
        private bool _IsStepTwoVisible = false;
        public bool IsStepTwoVisible
        {
            get { return _IsStepTwoVisible; }
            set { SetProperty(ref _IsStepTwoVisible, value, nameof(IsStepTwoVisible)); }
        }

        private Drug _SelectedDrugName;
        public Drug SelectedDrug
        {
            get { return _SelectedDrugName; }
            set
            {
                SetProperty(ref _SelectedDrugName, value, nameof(SelectedDrug));
                RunRefreshDrugSuggestions = false;
                IsStepThreeVisible = _SelectedDrugName != null;
            }
        }

        private bool _IsStepThreeVisible = false;
        public bool IsStepThreeVisible
        {
            get { return _IsStepThreeVisible; }
            set { SetProperty(ref _IsStepThreeVisible, value, nameof(IsStepThreeVisible)); }
        }


        private IEnumerable<string> _Warnings;
        public IEnumerable<string> Warnings
        {
            get { return _Warnings; }
            set { SetProperty(ref _Warnings, value, nameof(Warnings)); }
        }
        SemaphoreSlim SemaphoreSlimGetWarningsAsync = new SemaphoreSlim(1);
        public async Task<IEnumerable<string>> GetWarningsAsync()
        {
            try
            {
                await SemaphoreSlimGetWarningsAsync.WaitAsync();

                //get current drugs and append temporary the new one 
                List<Drug> currentDrugList = new List<Drug>(DrugService.Drugs);
                //refetch the drug with its precipitants
                SelectedDrug = _drugInteractionService.FindDrugByDrugBankId(SelectedDrug.DrugId);

                currentDrugList.Add(SelectedDrug);

                var warnings = await _drugInteractionService.AreDrugsInteractingAsync(currentDrugList);

                if (!warnings.Any())
                {
                    CanAddMedicine = true;
                }
                return warnings;
            }
            catch (Exception e)
            {

            }
            finally
            {
                SemaphoreSlimGetWarningsAsync.Release();
            }
            return Enumerable.Empty<string>();
        }

        private bool _CanAddMedicine = false;
        public bool CanAddMedicine
        {
            get { return _CanAddMedicine; }
            set { SetProperty(ref _CanAddMedicine, value, nameof(_CanAddMedicine)); }
        }

        private IEnumerable<Drug> _DrugSuggestions;
        public IEnumerable<Drug> DrugSuggestions
        {
            get { return _DrugSuggestions; }
            set { SetProperty(ref _DrugSuggestions, value, nameof(DrugSuggestions)); }
        }

        private DrugService DrugService { get; }
        public ICommand AddMedicineCommand { get; }

        protected async void OnAddMedicineCommand()
        {
            try
            {
                IsOnAddMedicineCommandProcessing = true;
                StopRefreshDrugSuggestion();
                Warnings = await GetWarningsAsync();
                if (CanAddMedicine && SelectedDrug != null)
                {
                    DrugService.AddDrug(SelectedDrug);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    NavigationService.NavigateAsync(nameof(MainPageViewModel));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                }
                else
                {
                    _dialogService.ShowDialog("asdsafd");
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowDialog(e.ToString());
            }
            finally
            {
                IsOnAddMedicineCommandProcessing = false;
            }
        }


        private bool _IsOnAddMedicineCommandProcessing;
        public bool IsOnAddMedicineCommandProcessing
        {
            get { return _IsOnAddMedicineCommandProcessing; }
            set { SetProperty(ref _IsOnAddMedicineCommandProcessing, value, nameof(IsOnAddMedicineCommandProcessing)); }
        }
    }
}
