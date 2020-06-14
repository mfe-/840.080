using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace DDILibrary
{
    /// <summary>
    /// Potential Drug Interaction Service
    /// </summary>
    public class DrugInteractionService
    {
        private readonly CsvParser<DrugDataSet> _csvParser;
        private readonly Func<Stream> _funcGetCsvStream;

        public DrugInteractionService()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, '\t');
            DrugDataSetMapping csvMapper = new DrugDataSetMapping();
            _csvParser = new CsvParser<DrugDataSet>(csvParserOptions, csvMapper);
        }
        public DrugInteractionService(string filePath) : this(
            () => new StreamReader(filePath, Encoding.ASCII)?.BaseStream)
        {
        }
        public DrugInteractionService(Func<Stream> funcGetCsvStream) : this()
        {
            _funcGetCsvStream = funcGetCsvStream;
        }
        /// <summary>
        /// this Stream will be disposed when accessing by <seealso cref="CsvParserExtensions.ReadFromStream"/>
        /// </summary>
        public Stream Stream
        {
            get
            {
                return _funcGetCsvStream?.Invoke();
            }
        }
        public Drug FindDrug(string drugName)
        {
            return FindDrugByPredict(a => a.Result.Object.ToLowerInvariant().Contains(drugName));
        }
        /// <summary>
        /// Finds the 
        /// </summary>
        /// <param name="drugBankId">For example http://bio2rdf.org/drugbank:DB01175</param>
        /// <returns>The first match which is found in the datasource</returns>
        public Drug FindDrugByDrugBankId(string drugBankId)
        {
            return FindDrugByPredict(a => a.Result.Drug1 == drugBankId);
        }
        /// <summary>
        /// Finds the overgiven drug
        /// </summary>
        /// <remarks>If more than one result is found null will be returned.</remarks>
        /// <param name="drugName"></param>
        /// <returns></returns>
        public Drug FindDrugByPredict(Predicate<CsvMappingResult<DrugDataSet>> lookupPattern)
        {
            Drug drug = null;
            var drugDataSets = _csvParser.ReadFromStream(Stream, Encoding.ASCII)
                .Where(a => a?.Result?.Object != null
                    && lookupPattern.Invoke(a))
                        .Select(a => a.Result)
                            .ToArray();

            if (drugDataSets.Select(a => a.Drug1.ToLowerInvariant()).Distinct().Count() > 1)
            {
                drugDataSets = new DrugDataSet[0];
            }

            if (drugDataSets.Any())
            {
                //create the drug itself
                drug = ToDrug(drugDataSets.FirstOrDefault());

                foreach (DrugDataSet drugDataSet in drugDataSets)
                {
                    string drugIdPrecipitant = drugDataSet.Drug2;

                    //underneath code does not performe good so we remove it temporary
                    //var interactions = _csvParser.ReadFromStream(Stream, Encoding.ASCII)
                    //    .Where(a => a?.Result?.Drug1 != null
                    //        && a.Result.Drug1 == drugIdPrecipitant
                    //        && a.Result.Drug2 == drug.DrugId)
                    //            .Select(a => a.Result)
                    //                .ToArray();
                    //drug.Precipitant.Add(ToPrecipitant(interactions, drugDataSet.Precipitant));
                    drug.Precipitant.Add(ToPrecipitant(drugDataSet));
                }
            }

            return drug;
        }
        public Task<Drug> FindDrugAsync(string drugName)
        {
            return Task.Run(() => FindDrug(drugName));
        }


        private static Drug ToDrug(DrugDataSet drugDataSet)
        {
            var drug = new Drug();
            drug.Name = drugDataSet.Object.ToLowerInvariant();
            drug.DrugId = drugDataSet.Drug1;
            return drug;
        }
        private static Drug ToPrecipitant(DrugDataSet drugDataSet)
        {
            var drug = new Drug();
            drug.Name = drugDataSet.Precipitant.ToLowerInvariant();
            drug.DrugId = drugDataSet.Drug2;
            drug.WarningText = drugDataSet.Label;
            return drug;
        }
        /// <summary>
        /// Merges the drugDataSets and produces an instance of <seealso cref="Drug"/>
        /// </summary>
        /// <param name="drugDataSets"></param>
        /// <param name="precipitantName"></param>
        /// <returns></returns>
        private static Drug ToPrecipitant(IEnumerable<DrugDataSet> drugDataSets, string precipitantName)
        {
            var drug = new Drug();
            foreach (var drugDataSet in drugDataSets)
            {
                if (drug.Name == null)
                {
                    drug.Name = precipitantName;
                    drug.DrugId = drugDataSet.Drug2;
                }
                if (drugDataSet.Label != "None")
                {
                    drug.WarningText = drugDataSet.Label;
                }
                //if (drugDataSet.Severity != "None")
                //{
                //    drug.Severity = drugDataSet.Label;
                //}

            }
            return drug;
        }
        /// <summary>
        /// Returns suggested drugs by the overgiven name.
        /// </summary>
        /// <param name="name">the name to lookup</param>
        /// <param name="max"></param>
        /// <returns>The returned records contains only data for the fields Object and Drug1</returns>
        public Drug[] SuggestDrugs(string name, int max = 3)
        {
            name = name.ToLowerInvariant();

            var query = _csvParser.ReadFromStream(Stream, Encoding.ASCII)
                    .Where(a => a?.Result?.Object != null
                        && ((a.Result.Object.ToLowerInvariant().StartsWith(name)) || a.Result.Object.ToLowerInvariant().Contains(name)))
                                .Select(s => ToDrug(s.Result))
                                    .DistinctBy(d => d.DrugId)
                                        .ToArray();
            return query;
        }
        public IEnumerable<string> AreDrugsInteracting(IEnumerable<Drug> usedDrugs)
        {
            List<string> warnings = new List<string>();
            foreach (Drug currentDrug in usedDrugs)
            {
                //get all drugs without the current one
                var possiblePrecipitantDrugs = usedDrugs.Where(a => a.DrugId != currentDrug.DrugId);
                //check if the other drugs are occouring in the currents drug Precipitants list
                foreach (Drug precipitantDrug in possiblePrecipitantDrugs)
                {
                    var ddi = currentDrug.Precipitant.FirstOrDefault(a => a.DrugId == precipitantDrug.DrugId);
                    if (ddi != null)
                    {
                        //add warning
                        warnings.Add(ddi.WarningText);
                    }
                }
            }

            //foreach (var drug in usedDrugs)
            //{
            //    //get all Precipitants of the current drug
            //    var prec = _csvParser.ReadFromStream(Stream, Encoding.ASCII)
            //        .Where(a => a?.Result?.Object != null
            //            && a.Result.Object.ToLowerInvariant().Contains(drug?.Object?.ToLowerInvariant()))
            //                .Select(a => a.Result).ToArray();
            //    //check if the current drug.Precipitants (prec) occurre in another drug (usedDrugs) which are used 
            //    var interactions = prec.Where(a =>
            //            usedDrugs.Where(c => c != drug) //without current drug
            //                .Any(b => b.Object.ToLowerInvariant() == a.Precipitant.ToLowerInvariant()
            //        ));
            //    //generate warnings depending whether label is et or not
            //    warnings.AddRange(
            //        interactions
            //            .Select(a => a.Label == "None" ? $"Possible interaction {a.Object} for {a.Precipitant}" : a.Label)
            //                .Distinct()
            //                    .ToArray());
            //}
            return warnings;
        }
        public Task<IEnumerable<string>> AreDrugsInteractingAsync(IEnumerable<Drug> usedDrugs)
        {
            return Task.Run(() => AreDrugsInteracting(usedDrugs));
        }
    }
    public static class Linq
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
