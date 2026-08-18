using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        string path = "habits.json";
        List<string> habits = new List<string>();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            habits = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }

        while (true)
        {
            Console.WriteLine("\n=== HABIT TRACKER ===");
            Console.WriteLine("1. Добавить привычку");
            Console.WriteLine("2. Показать привычки");
            Console.WriteLine("3. Выход");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Введите число от 1 до 3");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Введите привычку: ");
                    string habit = Console.ReadLine()?.Trim() ?? string.Empty;
                    if (!string.IsNullOrWhiteSpace(habit))
                    {
                        habits.Add(habit);
                        Console.WriteLine("Добавлено!");
                    }
                    else
                    {
                        Console.WriteLine("Название не может быть пустым.");
                    }
                    break;

                case 2:
                    if (habits.Count == 0)
                    {
                        Console.WriteLine("Список привычек пуст.");
                    }
                    else
                    {
                        Console.WriteLine("Ваши привычки:");
                        for (int i = 0; i < habits.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {habits[i]}");
                        }
                    }
                    break;

                case 3:
                    string jsonOut = JsonSerializer.Serialize(habits);
                    File.WriteAllText(path, jsonOut);
                    Console.WriteLine("Данные сохранены. До свидания!");
                    return;
            }
        }
    }
}