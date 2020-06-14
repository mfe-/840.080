using DDILibrary;
using Prism;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DDIApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new DDIApp.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            string filename = "CombinedDatasetConservativeTWOSIDES.csv";
            FileInfo fileInfo = new FileInfo($"{Environment.CurrentDirectory}\\Assets\\{filename}");
            // Register any platform specific implementations
            DrugInteractionService drugInteractionService = new DrugInteractionService(fileInfo.FullName);

            containerRegistry.RegisterInstance(drugInteractionService);
        }
    }
}
