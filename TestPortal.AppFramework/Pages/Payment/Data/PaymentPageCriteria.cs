using Browser.Core.Framework;

namespace TP.AppFramework
{
    public class PaymentPageCriteria
    {
        public readonly ICriteria<PaymentPage> UseADiscountCodeLnkVisible = new Criteria<PaymentPage>(p =>
        {
            return p.Exists(Bys.PaymentPage.UseADiscountCodeLnk, ElementCriteria.IsVisible);

        }, "Use A Discount Code link visible");

        public readonly ICriteria<PaymentPage> PageReady;

        public PaymentPageCriteria()
        {
            PageReady = UseADiscountCodeLnkVisible;
        }
    }
}
