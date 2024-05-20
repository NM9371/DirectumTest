using DirectumTest;
using System.Text;

class Program
{
    static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Console.OutputEncoding = Encoding.GetEncoding(866);
        Console.InputEncoding = Encoding.GetEncoding(866);
        MeetingsManager meetingsManager = new MeetingsManager();

        while (true)
        {
            Console.WriteLine("1. Создать новую встречу");
            Console.WriteLine("2. Просмотреть встречи за дату");
            Console.WriteLine("3. Изменить встречу");
            Console.WriteLine("4. Удалить встречу");
            Console.WriteLine("5. Экспорт встреч за дату");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddMeeting(meetingsManager);
                    break;
                case "2":
                    ViewMeetings(meetingsManager);
                    break;
                case "3":
                    ChangeMeeting(meetingsManager);
                    break;
                case "4":
                    DeleteMeeting(meetingsManager);
                    break;
                case "5":
                    ExportMeetings(meetingsManager);
                    break;
                default:
                    break;
            }
        }
    }

    static void AddMeeting(MeetingsManager manager)
    {
        Console.Write("Введите название встречи: ");
        string meetingName = Console.ReadLine();
        Console.Write("Введите описание (или оставьте пустым): ");
        string? meetingDescription = Console.ReadLine();
        meetingDescription = meetingDescription == string.Empty ? null : meetingDescription;
        Console.Write("Введите время начала встречи в формате {yyyy-MM-dd HH:mm}: ");
        DateTime meetingStart = DateTime.Parse(Console.ReadLine().ToString());
        Console.Write("Введите длительность встречи в минутах: ");
        int meetingDuration = int.Parse(Console.ReadLine());
        Console.Write("Введите время до начала встречи в минутах для уведомления (или оставьте пустым): ");
        string notifyBeforeMeetingString = Console.ReadLine();
        int? notifyBeforeMeeting;
        if (notifyBeforeMeetingString == string.Empty || int.Parse(notifyBeforeMeetingString) == 0)
        {
            notifyBeforeMeeting = null;
        }
        else
        {
            notifyBeforeMeeting = int.Parse(notifyBeforeMeetingString);
        }

        Meeting meeting = new Meeting(meetingName, meetingDescription, meetingStart, meetingDuration, notifyBeforeMeeting);
       
        try
        {
            int id = manager.AddMeeting(meeting);
            Console.WriteLine($"Встреча {id} добавлена");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    static void ChangeMeeting(MeetingsManager manager)
    {
        Console.Write("Введите ID встречи: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Введите название встречи: ");
        string meetingName = Console.ReadLine();
        Console.Write("Введите описание (или оставьте пустым): ");
        string? meetingDescription = Console.ReadLine();
        meetingDescription = meetingDescription == string.Empty ? null : meetingDescription;
        Console.Write("Введите время начала встречи в формате {yyyy-MM-dd HH:mm}: ");
        DateTime meetingStart = DateTime.Parse(Console.ReadLine().ToString());
        Console.Write("Введите длительность встречи в минутах: ");
        int meetingDuration = int.Parse(Console.ReadLine());
        Console.Write("Введите время до начала встречи в минутах для уведомления (или оставьте пустым): ");
        string notifyBeforeMeetingString = Console.ReadLine();
        int? notifyBeforeMeeting;
        if (notifyBeforeMeetingString == string.Empty || int.Parse(notifyBeforeMeetingString) == 0)
        {
            notifyBeforeMeeting = null;
        }
        else
        {
            notifyBeforeMeeting = int.Parse(notifyBeforeMeetingString);
        }

        try
        {
            manager.UpdateMeeting(id, meetingName, meetingDescription, meetingStart, meetingDuration, notifyBeforeMeeting);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine("Встреча изменена");
    }

    static void DeleteMeeting(MeetingsManager manager)
    {
        Console.Write("Введите ID встречи для удаления: ");
        int id = int.Parse(Console.ReadLine());
        try
        {
            manager.DeleteMeeting(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        Console.WriteLine("Встреча удалена");
    }

    static void ViewMeetings(MeetingsManager manager)
    {
        Console.Write("Введите дату в формате {yyyy-MM-dd}: ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        var meetings = manager.GetMeetingsByDate(date);
        if (meetings.Count == 0)
        {
            Console.WriteLine($"Не найдено встреч за {date}");
            return;
        }
        Console.WriteLine($"Встречи за {date}");
        meetings.ForEach(m => Console.WriteLine(m.ToString()));
    }

    static void ExportMeetings(MeetingsManager manager)
    {
        Console.Write("Введите дату в формате {yyyy-MM-dd}: ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.Write("Введите путь к файлу для экспорта в формате {C:\\folder\\filename.txt}: ");
        string filePath = Console.ReadLine();

        try
        {
            manager.ExportMeetings(date, filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Встречи экспортированы");
    }
}
