using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static string path = "habits.json";
    static List<Habit> habits = new List<Habit>();
    static void Main()
    {
        LoadHabits();

        while (true)
        {
            ShowMenu();
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 5)
            {
                Print("Введите число от 1 до 5");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddHabit();
                    break;
                case 2:
                    ShowHabits();
                    break;
                case 3:
                    DeleteHabit();
                    break;
                case 4:
                    MarkHabitAsCompleted();
                    break;
                case 5:
                    Exit();
                    break;
            }
        }
    }
    static bool loadSuccessful = true;
    static void LoadHabits()
    {
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                habits = JsonSerializer.Deserialize<List<Habit>>(json) ?? new List<Habit>();
            }
        }
        catch (Exception ex)
        {
            Print($"Ошибка при загрузке привычек: {ex.Message}");
            loadSuccessful = false;

        }
    }
    static void SaveHabits()
    {
        string json = JsonSerializer.Serialize(habits);
        File.WriteAllText(path, json);
    }
    static void ShowMenu()
    {
        Print("\n=== HABIT TRACKER ===");
        Print("1. Добавить привычку");
        Print("2. Показать привычки");
        Print("3. Удалить привычку");
        Print("4. Отметить привычку как завершенную");
        Print("5. Выход");
    }
    static void AddHabit()
    {
    if (!loadSuccessful)
    {
        Print("Предупреждение: предыдущая загрузка привычек завершилась с ошибкой. Новые привычки могут не сохраниться.");
        return;
    }
    
    Print("Введите привычку: ");
    string title = Console.ReadLine() ?? string.Empty;

    if (!string.IsNullOrWhiteSpace(title))
    {
        habits.Add(new Habit { Title = title });
        SaveHabits();
        Print("Привычка добавлена.");
    }
    else
    {
        Print("Название не может быть пустым.");
    }
    }
    static void ShowHabits()
    {
        if (habits.Count == 0)
        {
            Print("Список привычек пуст.");
        }
        else
        {
            Print("Список привычек:");
            foreach (var habit in habits)
            {
                Print($"- {habit.Title} (Завершено: {habit.IsCompleted})");
            }
        }
    }
    static void DeleteHabit()
    {
    if (habits.Count == 0)
    {
        Print("Список привычек пуст.");
        return;
    }

    Print("Список привычек:");
    
    ShowHabitsForSelection();

    Print("Введите номер привычки для удаления:");

    if (int.TryParse(Console.ReadLine(), out int index) &&
        index > 0 &&
        index <= habits.Count)
    {
        Print($"Удалить привычку \"{habits[index - 1].Title}\"? (y/n)");

        string? confirm = Console.ReadLine();

        if (confirm?.ToLower() == "y")
        {
            habits.RemoveAt(index - 1);
            Print("Привычка удалена.");
            SaveHabits();
        }
        else
        {
            Print("Удаление отменено.");
        }
    }
    else
    {
        Print("Неверный номер привычки.");
    }
    }
    static void MarkHabitAsCompleted()
    {
    if (habits.Count == 0)
    {
        Print("Список привычек пуст.");
        return;
    }

    Print("Список привычек:");

    ShowHabitsWithNumbers();

    Print("Введите номер привычки для отметки как завершенной:");

    if (int.TryParse(Console.ReadLine(), out int index) &&
        index > 0 &&
        index <= habits.Count)
    {
        if (habits[index - 1].IsCompleted)
        {
            Print("Эта привычка уже завершена.");
        }
        else
        {
            habits[index - 1].IsCompleted = true;
            SaveHabits();
            Print("Привычка отмечена как завершенная.");
        }
    }
    else
    {
        Print("Неверный номер привычки.");
    }
    }
    static void Exit()
    {
        SaveHabits();
        Print("Выход из программы.");
        Environment.Exit(0);
    }
    static void Print(string message)
    {
        Console.WriteLine(message);
    }
    static void ShowHabitsWithNumbers()
    {
    for (int i = 0; i < habits.Count; i++)
    {
        Print($"{i + 1}. {habits[i].Title} (Завершено: {habits[i].IsCompleted})");
    }
    } 
    static void ShowHabitsForSelection()
    {
        for (int i = 0; i < habits.Count; i++)
        {
            Print($"{i + 1}. {habits[i].Title}");
        }
    }
}
class Habit
{
    public string Title  { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}