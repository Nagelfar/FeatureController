using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeatureController.Features.Notification
{
    public class NotificationController:Controller
    {
        public ActionResult List()
        {
            var random = new Random();

            var messages = Enumerable.Range(3, random.Next(15))
                .Select(x=>"Message "+x);
            
            return View(messages.ToList());
        }

    }
}