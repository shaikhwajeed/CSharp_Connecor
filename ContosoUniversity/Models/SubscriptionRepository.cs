using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    /// <summary>
    /// Represents the subscription repository class which stores the temporary data.
    /// </summary>
    public static class SubscriptionRepository
    {
        public static List<Subscription> Subscriptions { get; set; }

        static SubscriptionRepository()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}