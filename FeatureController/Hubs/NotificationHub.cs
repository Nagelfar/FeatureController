using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FeatureController.Hubs
{
    public class NotificationHub : Hub
    {
        private static Timer _timer;
        public static void StartNotification()
        {
            var counter = 0;
            _timer = new Timer(s =>
            {
                SendMessage("huhu " + counter++);
            }, null, 10000, 5000);

        }

        public static void SendMessage(string message)
        {
            GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients.All.sendMessage(message);
        }
    }
}