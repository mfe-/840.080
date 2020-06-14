using DDIApp.Dialog;
using DDIApp.ViewModels;
using DDIApp.Views;
using DDILibrary;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DDIApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(MainPageViewModel)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            DrugService drugService = new DrugService();
            drugService.LoadDrugs();

            containerRegistry.RegisterInstance(drugService);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(nameof(MainPageViewModel));
            containerRegistry.RegisterForNavigation<CheckDrugInteractionPage, CheckDrugInteractionPageViewModel>(nameof(CheckDrugInteractionPageViewModel));
            containerRegistry.RegisterForNavigation<AddMedicinePage, AddMedicinePageViewModel>(nameof(AddMedicinePageViewModel));
            containerRegistry.RegisterForNavigation<TakePillPage, TakePillPageViewModel>(nameof(TakePillPageViewModel));
            containerRegistry.RegisterForNavigation<DrugPage, DrugPageViewModel>(nameof(DrugPageViewModel));

            containerRegistry.RegisterDialog<DDIWarningView, DDIWarningViewViewModel>(nameof(DDIWarningViewViewModel));

        }
    }
}
