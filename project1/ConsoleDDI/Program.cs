
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ConsoleDDI
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            string file = "CombinedDatasetConservativeTWOSIDES.csv";

            FileInfo fileInfo = new FileInfo(file);
            if (!fileInfo.Exists) throw new FileNotFoundException($"Please copy the file {file} to {Environment.CurrentDirectory}", file);

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, '\t');
            DrugDataSetMapping csvMapper = new DrugDataSetMapping();
            CsvParser<DrugDataSet> csvParser = new CsvParser<DrugDataSet>(csvParserOptions, csvMapper);

            Console.WriteLine("Loading data");
            List<DrugDataSet> drugDataSets = csvParser.ReadFromFile(file, Encoding.ASCII).Select(a => a.Result).ToList();

            Console.WriteLine("Please enter your drug!");
            List<DrugDataSet> usedDrugs = new List<DrugDataSet>();

            //user inputs used drugs
            while (true)
            {
                string line = Console.ReadLine();
                line = line.ToLower();
                if (String.IsNullOrEmpty(line))
                {
                    break;
                }

                DrugDataSet drugDataSet = drugDataSets.FirstOrDefault(a => a.Object.ToLowerInvariant().Contains(line));
                if (drugDataSet == null)
                {
                    Console.WriteLine("Drug not found, try a new one or abort by pressing enter.");
                }
                else
                {
                    if (line == drugDataSet.Object)
                    {
                        usedDrugs.Add(drugDataSet);
                    }
                    else
                    {
                        Console.WriteLine($"Did you mean {drugDataSet.Object}? [y/n]");
                        string yes = Console.ReadLine();
                        if ("y" == yes)
                        {
                            usedDrugs.Add(drugDataSet);
                        }
                    }

                }
            }
            Console.WriteLine("Drug Drug interaction check");
            //check drug interactions
            foreach (var drug in usedDrugs)
            {
                IEnumerable<DrugDataSet> possiblePrecipitant = usedDrugs.Where(a => a.Precipitant.ToLowerInvariant().Contains(drug.Object.ToLowerInvariant()));
                if (possiblePrecipitant.Any())
                {
                    Console.WriteLine($"{drug.Object} precipitant with {string.Join(", ", possiblePrecipitant.Select(a => a.Object))}");
                }

            }

        }
        public class DrugDataSetMapping : CsvMapping<DrugDataSet>
        {
            public DrugDataSetMapping() : base()
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

