using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DDIApp.ViewModels
{
    public class DDIWarningViewViewModel : BindableBase, IDialogAware, IAutoInitialize
    {
        public event Action<IDialogParameters> RequestClose;
        public DDIWarningViewViewModel()
        {
            AbortCommand = new DelegateCommand(OnAbortCommand);
            AddDrugCommand = new DelegateCommand(OnAddDrugCommand);
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Warnings = parameters.GetValue<IEnumerable<string>>(nameof(Warnings));
        }

        private IEnumerable<String> _Warnings;
        public IEnumerable<String> Warnings
        {
            get { return _Warnings; }
            set { SetProperty(ref _Warnings, value, nameof(Warnings)); }
        }

        public ICommand AbortCommand { get; }

        protected void OnAbortCommand()
        {
            RequestClose?.Invoke(new DialogParameters() { { "Result", false } });
        }

        public ICommand AddDrugCommand { get; }

        protected void OnAddDrugCommand()
        {
            RequestClose?.Invoke(new DialogParameters() { { "Result", true } });
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

    }
}
