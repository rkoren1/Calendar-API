namespace signalr.Data.Dtos
{
    public class CalendarEventDto
    {
        public string Subject { get; set; }
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Place { get; set; }
        public bool? AllDay { get; set; } = false;
        public bool? Repetable { get; set; } = false;

    }
}
