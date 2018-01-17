using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Stripe;

[assembly: OwinStartup(typeof(CSCAssignment1.Startup))]

namespace CSCAssignment1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Task 5 Stripe Secret Key.
            StripeConfiguration.SetApiKey("sk_test_uVwmQZgDyde86Qks7CbKAqiR");

            ConfigureAuth(app);
        }
    }
}
