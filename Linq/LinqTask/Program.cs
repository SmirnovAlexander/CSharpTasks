using System;

namespace LinqTask
{
    class Program
    {
        static void Main(string[] args)
        {

            BusinessLogic bl = new BusinessLogic();

            Console.WriteLine("LinqTask");
            Console.WriteLine("----------");

            Console.WriteLine("Users list: ");
            foreach (var user in bl.users)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Record list: ");
            foreach (var record in bl.records)
            {
                Console.WriteLine(record);
            }

            string input;
            MenuPrint();

            while (true)
            {
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
