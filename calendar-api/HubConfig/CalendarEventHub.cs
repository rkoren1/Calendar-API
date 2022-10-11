using Microsoft.AspNetCore.SignalR;
using signalr.Data;

namespace signalr.HubConfig
{
    public class CalendarEventHub : Hub<ICalendarEventHub>
    {
        public async Task NewCalendarEvent(CalendarEvent calendarEvent)
        {
            await Clients.Others.NewCalendarEvent(calendarEvent);
        }
        public async Task DeleteCalendarEvent(int Id)
        {
            await Clients.All.DeleteCalendarEvent(Id);
        }
    }
}
