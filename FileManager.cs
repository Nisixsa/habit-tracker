using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Project.HabitTracker;

public static class FileManager
{
    private static readonly string path = "habits.json";
    public static bool IsLoadSuccessful { get; private set; } = true;
    public static AppData LoadAppData()
    {
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<AppData>(json) ?? new AppData();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
            IsLoadSuccessful = false;
        }
        return new AppData();
    }
    public static void SaveAppData(AppData data)
    {
        if (!IsLoadSuccessful)
        {
            Console.WriteLine("Сохранение заблокировано: предыдущая загрузка завершилась с ошибкой.");
            return;
        }

        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }
}