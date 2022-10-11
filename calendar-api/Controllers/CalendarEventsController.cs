using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using signalr.Data;
using signalr.Data.Dtos;
using signalr.HubConfig;

namespace signalr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly CalendarDbContext _context;
        private readonly IHubContext<CalendarEventHub, ICalendarEventHub> _hubContext;

        public CalendarEventsController(CalendarDbContext context, IHubContext<CalendarEventHub, ICalendarEventHub>  hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/CalendarEvents
        [Route("GetCalendarEvents")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEvent>>> GetCalendarEvents()
        {
            return await _context.CalendarEvents.ToListAsync();
        }

        [Route("GetCalendarEvent/{id}")]
        [HttpGet]
        public async Task<ActionResult<CalendarEvent>> GetCalendarEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);

            if (calendarEvent == null)
            {
                return NotFound();
            }

            return calendarEvent;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("UpdateCalendarEvent/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutCalendarEvent(int id, CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendarEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                //trigger signalr
                await _hubContext.Clients.All.NewCalendarEvent(calendarEvent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("PostCalendarEvent")]
        [HttpPost]
        public async Task<ActionResult<CalendarEventDto>> PostCalendarEvent(CalendarEventDto calendarEvent)
        {
            CalendarEvent newCalendar = new CalendarEvent()
            {
                Start = calendarEvent.Start,
                End = calendarEvent.End,
                Description = calendarEvent.Description,
                Place = calendarEvent.Place,
                AllDay = calendarEvent.AllDay,
                Repetable = calendarEvent.Repetable,
                Subject = calendarEvent.Subject
            };
            _context.CalendarEvents.Add(newCalendar);

            try
            {
                await _context.SaveChangesAsync();

                //trigger signalr
                await _hubContext.Clients.All.NewCalendarEvent(newCalendar) ;
            }
            catch
            {
                return BadRequest();
            }

            return Ok(calendarEvent);

        }

        [Route("DeleteCalendarEvent/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCalendarEvent(int id)
        {
            var calendarEvent = await _context.CalendarEvents.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            _context.CalendarEvents.Remove(calendarEvent);

            try
            {
                await _context.SaveChangesAsync();

                //trigger signalr
                await _hubContext.Clients.All.DeleteCalendarEvent(id);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        private bool CalendarEventExists(int id)
        {
            return _context.CalendarEvents.Any(e => e.Id == id);
        }
    }
}
