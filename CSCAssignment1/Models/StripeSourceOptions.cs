using Stripe;

namespace CSCAssignment1.Controllers
{
    internal class StripeSourceOptions : SourceCard
    {
        public string TokenId { get; set; }
    }
}