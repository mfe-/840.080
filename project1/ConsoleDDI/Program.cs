
using DDILibrary;
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
            if (!fileInfo.Exists)
            {
                while (!new FileInfo(file).Exists)
                {
                    Console.WriteLine($"Error: Missing file {file}.{Environment.NewLine}Please copy {file} {Environment.NewLine}to: {Environment.CurrentDirectory}{Environment.NewLine}and Press Enter to continiue.");
                    Console.ReadLine();
                }
            }

            DrugInteractionService drugInteractionService = new DrugInteractionService(fileInfo.FullName);

            Console.WriteLine("Loading data");
            //List<DrugDataSet> drugDataSets = csvParser.ReadFromFile(file, Encoding.ASCII).Select(a => a.Result).ToList();

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

                DrugDataSet? drugDataSet = drugInteractionService.FindDrug(line);

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

            var warnings = drugInteractionService.AreDrugsInteracting(usedDrugs);

            //check drug interactions
            foreach (var warning in warnings)
            {
                Console.WriteLine(warning);

            }

        }
    }
}

