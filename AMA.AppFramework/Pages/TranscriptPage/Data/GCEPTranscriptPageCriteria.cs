using Browser.Core.Framework;

namespace AMA.AppFramework
{
    public class GCEPTranscriptPageCriteria
    {
        public readonly ICriteria<GCEPTranscriptPage> HeaderTbl = new Criteria<GCEPTranscriptPage>(p =>
        {
            return p.Exists(Bys.GCEPTranscriptPage.TranscriptHeaderTbl, ElementCriteria.IsVisible);

        }, "Username text box  visible");

        public readonly ICriteria<GCEPTranscriptPage> LoadIcon = new Criteria<GCEPTranscriptPage>(p =>
        {
            return p.Exists(Bys.AMAPage.LoadIcon, ElementCriteria.IsNotVisible);

        }, "Password is enabled");

        public readonly ICriteria<GCEPTranscriptPage> PageReady;

        public GCEPTranscriptPageCriteria()
        {
            PageReady = HeaderTbl.AND(LoadIcon);
        }
    }
}
