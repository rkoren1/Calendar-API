using Microsoft.AspNetCore.SignalR;
using signalr.Data;

namespace signalr.HubConfig
{
    public class CalendarEventHub : Hub<ICalendarEventHub>
    {
        public async Task NewCalendarEvent(CalendarEvent calendarEvent)
        {
            await Clients.All.NewCalendarEvent(calendarEvent);
        }
        public void ReloadCalendar()
        {
            Clients.All.ReloadCalendar();
        }
    }
}
