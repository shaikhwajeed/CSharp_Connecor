using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class ConnectorController : Controller
    {
        // GET: Callback
        public ViewResult Setup()
        {
            return View();
        }

        // GET: Callback
        public ActionResult Register()
        {
            var error = Request["error"];
            var state = Request["state"];
            if (!String.IsNullOrEmpty(error))
            {
                return RedirectToAction("Error", "Home", null);
            }
            else
            {
                var group = Request["group_name"];
                var webhook = Request["webhook_url"];

                Subscription sub = new Subscription();
                sub.GroupName = group;
                sub.WebHookUri = webhook;

                // Save the subscription so that it can be used to send the data to connectors.
                SubscriptionRepository.Subscriptions.Add(sub);

                return Redirect(state);
            }
        }
    }
}