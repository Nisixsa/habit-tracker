using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Project.HabitTracker;

public static class FileManager
{
    private static readonly string path = "habits.json";
    public static bool IsLoadSuccessful { get; private set; } = true;

    public static List<Habit> LoadHabits()
    {
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<Habit>>(json) ?? new List<Habit>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            IsLoadSuccessful = false;
        }
        return new List<Habit>();
    }

    public static void SaveHabits(List<Habit> habits)
    {
        if (!IsLoadSuccessful)
        {
            Console.WriteLine("Сохранение заблокировано: предыдущая загрузка завершилась с ошибкой.");
            return;
        }

        try
        {
            string json = JsonSerializer.Serialize(habits, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }
}