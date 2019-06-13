using System;
using System.Collections.Generic;

namespace LinqTask
{
    /// <summary>
    /// Class to communicate with user.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Initializing users and records lists.
            List<User> users = new List<User>();
            List<Record> records = new List<Record>();

            // Filling lists with test data.
            users.Add(new User(1, "Alexander", "Smirnov"));
            users.Add(new User(2, "Mark", "Philippov"));
            users.Add(new User(3, "Vyacheslav", "Borodkin"));
            users.Add(new User(4, "Feodor", "Zhilkin"));
            users.Add(new User(5, "Stepan", "Litvinov"));
            users.Add(new User(6, "Lut", "Smirnov"));
            users.Add(new User(7, "Feodor", "Kupriyanov"));

            records.Add(new Record(users[0], "Hello"));
            records.Add(new Record(users[1], "How are you?"));
            records.Add(new Record(users[2], "Fine, thanks"));
            records.Add(new Record(users[3], "Me too"));
            records.Add(new Record(users[4], "Good"));
            records.Add(new Record(users[4], "Goobye!"));

            // Initializing BusinessLogic instance.
            var bl = new BusinessLogic(users, records);

            Console.WriteLine("LinqTask");
            Console.WriteLine("----------");

            // Print user list.
            Console.WriteLine("Users list: ");
            foreach (var user in bl.users)
            {
                Console.WriteLine(user);
            }

            // Print records list.
            Console.WriteLine("Record list: ");
            foreach (var record in bl.records)
            {
                Console.WriteLine(record);
            }

            // Initialize an input variable.
            string input;

            // Print menu.
            MenuPrint();

            while (true)
            {
                // Reading input.
                input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("Введите фамилию: ");
                    foreach (var user in bl.GetUsersBySurname(Console.ReadLine()))
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "2")
                {
                    Console.WriteLine("Введите ID: ");
                    Console.WriteLine(bl.GetUserByID(Int32.Parse(Console.ReadLine())));
                }
                else if (input == "3")
                {
                    Console.WriteLine("Введите подстроку: ");
                    foreach (var user in bl.GetUsersBySubstring(Console.ReadLine()))
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "4")
                {
                    foreach (var user in bl.GetAllUniqueNames())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "5")
                {
                    foreach (var user in bl.GetAllAuthors())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "6")
                {
                    foreach (var elem in bl.GetUsersDictionary())
                    {
                        Console.WriteLine(elem);
                    }
                }
                else if (input == "7")
                {
                    Console.WriteLine(bl.GetMaxID());

                }
                else if (input == "8")
                {
                    foreach (var user in bl.GetOrderedUsers())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "9")
                {
                    foreach (var user in bl.GetDescendingOrderedUsers())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "10")
                {
                    foreach (var user in bl.GetReversedUsers())
                    {
                        Console.WriteLine(user);
                    }
                }
                else if (input == "11")
                {
                    Console.WriteLine("Введите размер страницы:");
                    int pageSize = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Введите номер страницы:");
                    int pageIndex = Int32.Parse(Console.ReadLine());
                    foreach (var user in bl.GetUsersPage(pageSize, pageIndex))
                    {
                        Console.WriteLine(user);
                    }
                }            
                else if (input == "0") break;
                else Console.WriteLine("Такой команды нет");
                MenuPrint();
            }
            Console.WriteLine("Нажмите любую клавишу для выхода из программы");
            Console.ReadKey();
        }

        // Printing menu.
        static void MenuPrint()
        {
            Console.WriteLine("-----*MENU*-----");
            Console.WriteLine("1: Выборка пользователей по фамилии");
            Console.WriteLine("2: Получение пользователя по ID");
            Console.WriteLine("3: Получение пользователей по подстроке");
            Console.WriteLine("4: Получение пользователей по уникальным именам");
            Console.WriteLine("5: Выборка пользователей, у которых есть сообщения");
            Console.WriteLine("6: Делаем словарь пользователей");
            Console.WriteLine("7: Максимальное значение ID");
            Console.WriteLine("8: Отсортированный список пользователей по имени");
            Console.WriteLine("9: Обратно отсортированный список пользователей по имени");
            Console.WriteLine("10: Отсортированный и перевёрнутый список пользователей по имени");
            Console.WriteLine("11: Пейджинг");
            Console.WriteLine("0: Выйти из программы");
            Console.WriteLine("----------------");
        }
    }
}
