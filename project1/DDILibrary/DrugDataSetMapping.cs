using TinyCsvParser.Mapping;

namespace DDILibrary
{
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
