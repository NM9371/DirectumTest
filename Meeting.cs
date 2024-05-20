namespace DirectumTest
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime End { get; set; }
        public TimeSpan? NotifyBeforeStart { get; set; }
        public bool IsNotificationSent { get; set; }
        public Meeting(string name, string? desctiption, DateTime start, int durationMinutes, int? notifyBeforeStartMinutes)
        {
            Name = name;
            Description = desctiption;
            Start = start;
            Duration = TimeSpan.FromMinutes(durationMinutes);
            End = Start+Duration;
            NotifyBeforeStart = notifyBeforeStartMinutes == null ? null : TimeSpan.FromMinutes(notifyBeforeStartMinutes.Value);
            IsNotificationSent = false;
        }

        public void ChangeNotifyBefore(int NotifyBeforeMinutes)
        {
            NotifyBeforeStart = TimeSpan.FromMinutes(NotifyBeforeMinutes);
        }

        public void Update(string name, string? desctiption, DateTime start, int durationMinutes, int? notifyBeforeStartMinutes)
        {
            Name = name;
            Description = desctiption;
            Start = start;
            Duration = TimeSpan.FromMinutes(durationMinutes);
            End = Start + Duration;
            NotifyBeforeStart = notifyBeforeStartMinutes == null ? null : TimeSpan.FromMinutes(notifyBeforeStartMinutes.Value);
        }

        public override string ToString()
        {
            string? description = Description == null ? null : " (" + Description + ")";
            string? notify = NotifyBeforeStart == null ? "Напоминание отключено" : $"Напоминание за {(int)NotifyBeforeStart.Value.TotalMinutes} минут";
            return $"{Id}; {Name}{description}; {Start} - {End}; {notify}";
        }
    }
}
