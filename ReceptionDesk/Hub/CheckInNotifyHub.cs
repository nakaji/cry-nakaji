using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Hubs;

namespace ReceptionDesk.Hub
{
    [HubName("checkInNotify")]
    public class CheckInNotifyHub : Microsoft.AspNet.SignalR.Hub
    {
    }
}