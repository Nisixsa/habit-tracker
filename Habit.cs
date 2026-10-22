namespace Project.HabitTracker;

public class Habit
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? LastCompletedDate { get; set; } = null;
    public virtual void ShowDetails()
    {
        Console.WriteLine($"{Title} (Завершено: {IsCompleted})");
    }
}
public class CounterHabit : Habit 
{
    public int CurrentCount { get; set; } = 0;
    public int TargetCount { get; set; } = 50;

    public override void ShowDetails()
    {
        Console.WriteLine($"{Title} | Прогресс: {CurrentCount}/{TargetCount} (Завершено: {IsCompleted})");
    }
}