using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LinqTask
{
    class BusinessLogic
    {
        public List<User> users = new List<User>();
        public List<Record> records = new List<Record>();
        public BusinessLogic()
        {
            //наполнение обеих коллекций тестовыми данными
            users.Add(new User(1, "Alexander", "Smirnov"));
            users.Add(new User(2, "Mark", "Filippov"));
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
        }

        //---ЗАПРОСЫ----

        //Выборка пользователей по имени
        public List<User> GetUsersBySurname(String surname)
        {
            return (from user in users where user.Surname == surname select user).ToList();
        }

        //Получение пользователя по ID
        public User GetUserByID(int id)
        {
            return (from user in users where user.ID == id select user).Single();
        }

        //Получение пользователя по подстроке
        public List<User> GetUsersBySubstring(String substring)
        {
            return (from user in users where user.Surname.Contains(substring) || user.Name.Contains(substring) select user).ToList();
        }

        //Выборка по уникальным именам
        public List<String> GetAllUniqueNames()
        {
            return (from user in users select user.Name).Distinct().ToList();
        }

        //Выборка пользователей, у которых есть сообщения
        public List<User> GetAllAuthors()
        {
            return (from record in records select record.Author).Distinct().ToList();
        }

        //Делаем словарь пользователей
        public Dictionary<int, User> GetUsersDictionary()
        {
            var keys = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            return keys.Zip(users, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);

        }

        //Максимальное значение ID
        public int GetMaxID()
        {
            var query = users.OrderByDescending(i => i.ID);
            return (from user in query select user.ID).ToList().First();
        }

        //Отсортированный список пользователей по имени
        public List<User> GetOrderedUsers()
        {
            return users.OrderBy(i => i.Name).ToList();
        }

        //Обратно отсортированный список пользователей по имени
        public List<User> GetDescendingOrderedUsers()
        {
            return users.OrderByDescending(i => i.Name).ToList();
        }

        //Отсортированный и перевёрнутый список пользователей по имени
        public List<User> GetReversedUsers()
        {
            return users.OrderBy(i => i.Name).Reverse().ToList();
        }

        //Пейджинг      
        public List<User> GetUsersPage(int pageSize, int pageIndex)
        {
            return users.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

    }
}
