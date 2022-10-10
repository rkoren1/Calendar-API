using Microsoft.EntityFrameworkCore;

namespace signalr.Data
{
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }
    }
}
