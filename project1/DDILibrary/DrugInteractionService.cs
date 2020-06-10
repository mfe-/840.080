using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace DDILibrary
{
    /// <summary>
    /// Potential Drug Interaction Service
    /// </summary>
    public class DrugInteractionService
    {
        private readonly CsvParser<DrugDataSet> _csvParser;
        private readonly string _filePath;
        public DrugInteractionService(string filePath)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, '\t');
            DrugDataSetMapping csvMapper = new DrugDataSetMapping();
            _csvParser = new CsvParser<DrugDataSet>(csvParserOptions, csvMapper);
            _filePath = filePath;
        }
        public DrugDataSet FindDrug(string drugName)
        {
            var drug = _csvParser.ReadFromFile(_filePath, Encoding.ASCII)
                .FirstOrDefault(a => a?.Result?.Object != null
                                && a.Result.Object.ToLowerInvariant().Contains(drugName))?.Result;
            return drug;
        }
        public IEnumerable<string> AreDrugsInteracting(IEnumerable<DrugDataSet> usedDrugs)
        {
            List<string> warnings = new List<string>();
            foreach (var drug in usedDrugs)
            {
                //get all Precipitants of the current drug
                var prec = _csvParser.ReadFromFile(_filePath, Encoding.ASCII)
                    .Where(a => a?.Result?.Object != null
                        && a.Result.Object.ToLowerInvariant().Contains(drug.Object.ToLowerInvariant()))
                            .Select(a => a.Result).ToArray();
                //check if the current drug.Precipitants (prec) occurre in another drug (usedDrugs) which are used 
                var interactions = prec.Where(a => 
                        usedDrugs.Where(c => c != drug) //without current drug
                            .Any(b => b.Object.ToLowerInvariant() == a.Precipitant.ToLowerInvariant()
                    ));
                //generate warnings depending whether label is et or not
                warnings.AddRange(
                    interactions
                        .Select(a => a.Label== "None" ? $"Possible interaction {a.Object} for {a.Precipitant}" : a.Label)
                            .Distinct()
                                .ToArray());
            }
            return warnings;
        }


    }
}
