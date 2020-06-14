using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DDILibrary
{
    [DataContract]
    [DebuggerDisplay("Name={Name}")]
    public class Drug : INotifyPropertyChanged
    {
        public Drug()
        {
            Precipitant = new List<Drug>();
        }
        private String _DrugId;
        [DataMember]
        public String DrugId
        {
            get { return _DrugId; }
            set { SetProperty(ref _DrugId, value, nameof(DrugId)); }
        }

        private String _Name;
        [DataMember]
        public String Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value, nameof(Name)); }
        }

        [DataMember]
        private IList<Drug> _Precipitant;
        public IList<Drug> Precipitant
        {
            get { return _Precipitant; }
            set { SetProperty(ref _Precipitant, value, nameof(Precipitant)); }
        }

        private String _WarningText;
        public String WarningText
        {
            get { return _WarningText; }
            set { SetProperty(ref _WarningText, value, nameof(WarningText)); }
        }

        //private String _Severity;
        //public String Severity
        //{
        //    get { return _Severity; }
        //    set { SetProperty(ref _Severity, value, nameof(Severity)); }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
