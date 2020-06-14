
using DDILibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            while (true)
            {
                DrugDrugInteractionCheck(fileInfo);
                Console.WriteLine("Start again? [y/n]");
                string yes = Console.ReadLine();
                if ("y" != yes)
                {
                    break;
                }
            }
        }

        private static void DrugDrugInteractionCheck(FileInfo fileInfo)
        {
            DrugInteractionService drugInteractionService = new DrugInteractionService(fileInfo.FullName);

            Console.WriteLine("Please enter your drug! (Be patient!)");
            List<Drug> usedDrugs = new List<Drug>();

            //user inputs used drugs
            while (true)
            {
                string line = Console.ReadLine();
                line = line.ToLower();
                if (String.IsNullOrEmpty(line))
                {
                    break;
                }

                Drug drug = drugInteractionService.FindDrug(line);

                if (drug == null)
                {
                    Console.WriteLine($"Drug \"{line}\" not found or too many results, try a new one or abort by pressing enter.");
                }
                else
                {
                    if (line == drug.Name)
                    {
                        usedDrugs.Add(drug);
                        Console.WriteLine("Enter the next drug or Press Enter to finish. (Be patient!)");
                    }
                    else
                    {
                        Console.WriteLine($"Did you mean {drug.Name}? [y/n]");
                        string yes = Console.ReadLine();
                        if ("y" == yes)
                        {
                            usedDrugs.Add(drug);
                            Console.WriteLine("Enter the next drug or Press Enter to Finish. (Be patient!)");
                        }
                    }

                }
            }
            Console.WriteLine("Drug Drug interaction check");

            var taskWarnings = drugInteractionService.AreDrugsInteractingAsync(usedDrugs);

            while (!taskWarnings.IsCompleted)
            {
                Console.Write(".");
            }
            Console.WriteLine("");
            //check drug interactions
            foreach (var warning in taskWarnings.Result)
            {
                Console.WriteLine(warning);
            }
        }
    }
}

