using System.Timers;

namespace DirectumTest
{
    public class MeetingsManager
    {
        private List<Meeting> Meetings;
        private System.Timers.Timer notifyTimer;

        public MeetingsManager()
        {
            Meetings = new List<Meeting>();
            notifyTimer = new System.Timers.Timer(10000);
            notifyTimer.Elapsed += CheckNotifications;
            notifyTimer.Start();
        }

        public void CheckNotifications(object sender, ElapsedEventArgs e)
        {
            var meetingsToNotify = Meetings.Where(m => !m.IsNotificationSent && m.Start-m.NotifyBeforeStart < DateTime.Now).ToList();
            meetingsToNotify.ForEach(m =>
            {
                Console.WriteLine($"Встреча {m.Name} начинается в {m.Start}");
                m.IsNotificationSent = true;
            });
        }
        public int Add(Meeting meeting)
        {
            if (meeting.Start < DateTime.Now)
            {
                throw new Exception("Невозможно добавить новую встречу. Выбранное время уже прошло.");
            }

            if (Meetings.Any(m => m.Start < meeting.End && m.End > meeting.Start))
            {
                throw new Exception("Невозможно добавить новую встречу. Выбранное время уже занято.");
            }

            if (Meetings.Count == 0)
            {
                meeting.Id = 1;
            }
            else
            {
                meeting.Id = Meetings.Max(m => m.Id) + 1;
            }

            Meetings.Add(meeting);
            return meeting.Id;
        }
        public void Update(int id, string name, string? desctiption, DateTime start, int durationMinutes, int? notifyBeforeStartMinutes)
        {
            var meeting = Meetings.FirstOrDefault(m => m.Id == id);
            if (meeting == null)
            {
                throw new Exception("Встреча с указанным ID не найдена");
            }

            meeting.Update(name, desctiption, start, durationMinutes, notifyBeforeStartMinutes);
        }

        public void Delete(int id)
        {
            int result = Meetings.RemoveAll(m => m.Id == id);
            if (result == 0)
            {
                throw new Exception("Встреча с указанным ID не найдена.");
            }
        }

        public List<Meeting> GetByDate(DateTime date)
        {
            var result = Meetings.Where(m => m.Start.Date == date.Date).ToList();

            if (result.Count == 0)
            {
                throw new Exception("Не найдены встречи за указанную дату.");
            }

            return result;
        }

        public void Export(DateTime date, string filePath)
        {
            var meetingsOnDate = GetByDate(date);
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                foreach (var meeting in meetingsOnDate)
                {
                    writer.WriteLine(meeting.ToString());
                }
            }
        }
    }
}
