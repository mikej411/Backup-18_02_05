using Browser.Core.Framework;

namespace NOF.AppFramework
{
    public class TranscriptPageCriteria
    {
        public readonly ICriteria<TranscriptPage> TranscriptLabelVisible = new Criteria<TranscriptPage>(p =>
        {
            return p.Exists(Bys.TranscriptPage.TranscriptLbl, ElementCriteria.IsVisible);

        }, "Transcript Label visible");


        public readonly ICriteria<TranscriptPage> PageReady;

        public TranscriptPageCriteria()
        {
            PageReady = TranscriptLabelVisible;
        }
    }
}
 
