
using System;
using System.IO;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ConsoleDDI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string file = "CombinedDatasetConservativeTWOSIDES.csv";

            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists) throw new FileNotFoundException($"Please copy the file {file} to {Environment.CurrentDirectory}", file);

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, '\t');
            CsvDDIDataSetMapping csvMapper = new CsvDDIDataSetMapping();
            CsvParser<DDIDataSet> csvParser = new CsvParser<DDIDataSet>(csvParserOptions, csvMapper);

            var result = csvParser.ReadFromFile(file, Encoding.ASCII);


        }
        public class CsvDDIDataSetMapping : CsvMapping<DDIDataSet>
        {
            public CsvDDIDataSetMapping()
                : base()
            {
                MapProperty(0, x => x.Drug1);
                MapProperty(1, x => x.Object);
                MapProperty(2, x => x.Drug2);
                MapProperty(3, x => x.Precipitant);
                //MapProperty(4, x => x.Certainty);
                //MapProperty(5, x => x.Contraindication);
                //MapProperty(6, x => x.DateAnnotated);
                //MapProperty(7, x => x.DdiPkEffect);
                //MapProperty(8, x => x.DdiPkMechanism);
                //MapProperty(9, x => x.EffectConcept);

                //MapProperty(10, x => x.Homepage);
                MapProperty(11, x => x.Label);
                //MapProperty(12, x => x.NumericVal);
                MapProperty(13, x => x.ObjectUri);
                MapProperty(14, x => x.Pathway);

                MapProperty(15, x => x.Precaution);
                MapProperty(16, x => x.PrecipUri);
                MapProperty(17, x => x.Severity);
                MapProperty(18, x => x.Uri);
                MapProperty(19, x => x.WhoAnnotated);
                MapProperty(20, x => x.Source);
                MapProperty(21, x => x.DdiType);
                MapProperty(22, x => x.Evidence);

            }
        }
    }
}

