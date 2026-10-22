namespace Project.HabitTracker;
public class AppData
{
    public List<Habit> Habits { get; set; } = new();
    public UserStats Stats { get; set; } = new();
}
public class UserStats
{
    public int GlobalStreak { get; set; } = 0;
    public int BestGlobalStreak { get; set; } = 0;
    public DateTime? LastActiveDate { get; set; } = null;
}