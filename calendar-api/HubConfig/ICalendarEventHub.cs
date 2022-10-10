using signalr.Data;

namespace signalr.HubConfig
{
    public interface ICalendarEventHub
    {
        Task NewCalendarEvent( CalendarEvent calendarEvent);
        Task ReloadCalendar();
    }

    

}