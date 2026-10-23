using System;
using System.Collections.Generic;

namespace Project.HabitTracker;

class Program
{
    static void Main(string[] args)
    {
        AppData appData = FileManager.LoadAppData();
        CheckDailyStreak(appData.Stats);

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== HABIT TRACKER ===");
            Console.WriteLine($"Глобальный стрик: {appData.Stats.GlobalStreak} дней | Рекорд: {appData.Stats.BestGlobalStreak} дней\n");
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить привычку");
            Console.WriteLine("2. Отметить выполнение");
            Console.WriteLine("3. Удалить привычку");
            Console.WriteLine("4. Посмотреть список привычек");
            Console.WriteLine("5. Выйти и сохранить");
            Console.Write("Выбери пункт: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":AddHabit(appData.Habits);break;
                case "2":CompleteHabit(appData);break;
                case "3":DeleteHabit(appData.Habits);break;
                case "4":Console.Clear();ShowHabitsList(appData.Habits);Console.WriteLine("\nНажмите любую клавишу...");Console.ReadKey();break;
                case "5":FileManager.SaveAppData(appData);running = false;break;
                default:Console.WriteLine("Неверный выбор.");Console.ReadKey();break;
            }
        }
    }

    static void CheckDailyStreak(UserStats stats)
    {
        if (stats.LastActiveDate == null) return;

        DateTime today = DateTime.Today;
        DateTime lastActive = stats.LastActiveDate.Value.Date;

        if ((today - lastActive).Days > 1)
        {
            stats.GlobalStreak = 0;
        }
    }
    static void AddHabit(List<Habit> habits)
    {
    Console.Write("Введите название привычки: ");
    string? title = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Название не может быть пустым!");
        Console.ReadKey();
        return;
    }
    while (true)
    {
        Console.Write("Будет ли у привычки счетчик? (y/n): ");
        string? answer = Console.ReadLine()?.Trim().ToLower();

        if (answer == "y")
        {
            while (true)
            {
                Console.Write("Введите целевое число: ");

                if (int.TryParse(Console.ReadLine(), out int target) && target > 0)
                {
                    habits.Add(new CounterHabit
                    {
                        Title = title,
                        TargetCount = target
                    });

                    Console.WriteLine("Привычка добавлена!");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Введите корректное число больше 0.");
            }
        }
        else if (answer == "n")
        {
            habits.Add(new Habit
            {
                Title = title
            });

            Console.WriteLine("Привычка добавлена!");
            Console.ReadKey();
            return;
        }
        else
        {
            Console.WriteLine("Можно вводить только y или n.");
        }
    }
    }
    static void CompleteHabit(AppData appData)
    {
        if (appData.Habits.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер привычки: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= appData.Habits.Count)
        {
            var habit = appData.Habits[index - 1];

            if (habit is CounterHabit counterHabit)
            {
                Console.Write($"Текущий прогресс: {counterHabit.CurrentCount}/{counterHabit.TargetCount}. Сколько добавить? ");
                if (int.TryParse(Console.ReadLine(), out int add) && add > 0)
                {
                    counterHabit.CurrentCount += add;
                    if (counterHabit.CurrentCount >= counterHabit.TargetCount)
                    {
                        counterHabit.IsCompleted = true;
                    }
                }
            }
            else
            {
                habit.IsCompleted = true;
            }

            habit.LastCompletedDate = DateTime.Now;
            UpdateGlobalStreak(appData.Stats);

            Console.WriteLine("Прогресс учтен!");
        }
        else
        {
            Console.WriteLine("Неверный номер.");
        }
        Console.ReadKey();
    }
    static void UpdateGlobalStreak(UserStats stats)
    {
        DateTime today = DateTime.Today;

        if (stats.LastActiveDate == null)
        {
            stats.GlobalStreak = 1;
        }
        else
        {
            DateTime lastActive = stats.LastActiveDate.Value.Date;

            if (lastActive == today)
            {
            }
            else if (lastActive == today.AddDays(-1))
            {
                stats.GlobalStreak++;
            }
            else if (lastActive < today.AddDays(-1))
            {
                stats.GlobalStreak = 1;
            }
        }

        stats.LastActiveDate = today;

        if (stats.GlobalStreak > stats.BestGlobalStreak)
        {
            stats.BestGlobalStreak = stats.GlobalStreak;
        }
    }
    static void DeleteHabit(List<Habit> habits)
    {
        if (habits.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            Console.ReadKey();
            return;
        }

        Console.Write("Введите номер для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= habits.Count)
        {
            habits.RemoveAt(index - 1);
            Console.WriteLine("Удалено!");
        }
        else
        {
            Console.WriteLine("Неверный номер.");
        }
        Console.ReadKey();
    }
    static void ShowHabitsList(List<Habit> habits)
    {
    Console.WriteLine("=== СПИСОК ПРИВЫЧЕК ===");

    if (habits.Count == 0)
    {
        Console.WriteLine("Список пуст.");
        return;
    }

    for (int i = 0; i < habits.Count; i++)
    {
        Console.Write($"{i + 1}. ");
        habits[i].ShowDetails();
    }
    }
}