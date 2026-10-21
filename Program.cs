namespace Project.HabitTracker;

class Program
{
    static List<Habit> habits = FileManager.LoadHabits();

    static void Main()
    {
        while (true)
        {
            ShowMenu();
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Введите число от 1 до 5");
                continue;
            }

            switch (choice)
            {
                case 1: AddHabit(); break;
                case 2: ShowHabits(); break;
                case 3: DeleteHabit(); break;
                case 4: CompleteHabit(); break;
                case 5: 
                    FileManager.SaveHabits(habits);
                    Console.WriteLine("Выход...");
                    return;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n=== HABIT TRACKER (OOP) ===");
        Console.WriteLine("1. Добавить привычку");
        Console.WriteLine("2. Показать привычки");
        Console.WriteLine("3. Удалить привычку");
        Console.WriteLine("4. Отметить как завершенную");
        Console.WriteLine("5. Выход");
    }

    static void AddHabit()
    {
        Console.Write("Введите название привычки: ");
        string? title = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(title))
        {
            habits.Add(new Habit { Title = title });
            FileManager.SaveHabits(habits);
            Console.WriteLine("Привычка добавлена!");
        }
    }

    static void ShowHabits()
    {
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

    static void DeleteHabit()
    {
        if (habits.Count == 0) return;
        
        ShowHabits();
        Console.Write("Номер для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= habits.Count)
        {
            habits.RemoveAt(idx - 1);
            FileManager.SaveHabits(habits);
            Console.WriteLine("Удалено.");
        }
    }

    static void CompleteHabit()
    {
        if (habits.Count == 0) return;

        ShowHabits();
        Console.Write("Номер выполненной привычки: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= habits.Count)
        {
            habits[idx - 1].IsCompleted = true;
            FileManager.SaveHabits(habits);
            Console.WriteLine("Отмечено!");
        }
    }
}