namespace Project.HabitTracker;

public class Habit
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;

    public virtual void ShowDetails()
    {
        Console.WriteLine($"{Title} (Завершено: {IsCompleted})");
    }
}
public class CounterHabits : Habit 
{
    public int CurrentCount { get; set; } = 0;
    public int TargetCount { get; set; } = 50;

    public override void ShowDetails()
    {
        Console.WriteLine($"{Title} | Прогресс: {CurrentCount}/{TargetCount} (Завершено: {IsCompleted})");
    }
}