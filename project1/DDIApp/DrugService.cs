using DDILibrary;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Essentials;

namespace DDIApp
{
    public class DrugService : BindableBase
    {
        private string _xmlFilePath;

        public DrugService()
        {
            Drugs = new List<Drug>();
            _xmlFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) }\\{DrugService.FileName}";
        }
        public DrugService(string xmlFilePath) : this()
        {
            _xmlFilePath = xmlFilePath;
        }
        public void AddDrug(Drug drug)
        {
            Drugs.Add(drug);
            SaveDrugs();
        }
        public void RemoveDrug(Drug drug)
        {
            Drugs.Remove(drug);
        }

        public IList<Drug> Drugs { get; private set; }

        public static string FileName = $"{nameof(Drug)}.xml";

        /// <summary>
        /// Load user settings which contains the user drugs
        /// </summary>
        /// <seealso cref="https://docs.microsoft.com/en-us/xamarin/essentials/preferences?tabs=android"/>
        public void LoadDrugs()
        {
            bool hasKey = new FileInfo(_xmlFilePath).Exists;
            //bool hasKey = Preferences.ContainsKey(nameof(Drugs));
            if (hasKey)
            {
                //string xml = Preferences.Get(nameof(Drugs), "");
                string xml;
                using (StreamReader file = new StreamReader(_xmlFilePath))
                {
                    xml = file.ReadToEnd();
                }

                if (!String.IsNullOrEmpty(xml))
                {
                    using (Stream stream = new MemoryStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(xml);
                        stream.Write(data, 0, data.Length);
                        stream.Position = 0;
                        DataContractSerializer deserializer = new DataContractSerializer(typeof(List<Drug>));
                        object drugs = deserializer.ReadObject(stream);

                        if (drugs is List<Drug> d)
                        {
                            Drugs = d;
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Saves the user drugs to the user settings
        /// </summary>
        public void SaveDrugs()
        {
            string serializedXml;
            using (MemoryStream memStm = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(List<Drug>));
                serializer.WriteObject(memStm, Drugs);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    serializedXml = streamReader.ReadToEnd();
                }
            }
            //Preferences.Set(nameof(Drugs), serializedXml);
            using (StreamWriter file = new StreamWriter(_xmlFilePath))
            {
                file.WriteLine(serializedXml);
            }
        }
    }
}
