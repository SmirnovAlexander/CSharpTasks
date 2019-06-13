using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTask
{
    /// <summary>
    /// Class to store logic.
    /// </summary>
    public class BusinessLogic
    {
        // Initializing class fields.
        public List<User> users = new List<User>();
        public List<Record> records = new List<Record>();

        public BusinessLogic(List<User> users, List<Record> records)
        {
            this.users = users;
            this.records = records;
        }

        // Choose users by surname.
        public List<User> GetUsersBySurname(string surname)
        {
            return (from user in users where user.Surname == surname select user).ToList();
        }

        // Choose users by ID.
        public User GetUserByID(int id)
        {
            return (from user in users where user.ID == id select user).Single();
        }

        // Get users by substring.
        public List<User> GetUsersBySubstring(string substring)
        {
            return (from user in users where user.Surname.Contains(substring) || user.Name.Contains(substring) select user).ToList();
        }

        // Get all unique users names.
        public List<string> GetAllUniqueNames()
        {
            return (from user in users select user.Name).Distinct().ToList();
        }

        // Get users who have messages.
        public List<User> GetAllAuthors()
        {
            return (from record in records select record.Author).Distinct().ToList();
        }

        // Make user dictionary.
        public Dictionary<int, User> GetUsersDictionary()
        {
            var keys = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            return keys.Zip(users, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
        }

        // Max ID.
        public int GetMaxID()
        {
            var query = users.OrderByDescending(i => i.ID);
            return (from user in query select user.ID).ToList().First();
        }

        // Sorted by name user list.
        public List<User> GetOrderedUsers()
        {
            return users.OrderBy(i => i.Name).ToList();
        }

        // Sorted by name user list by descend.
        public List<User> GetDescendingOrderedUsers()
        {
            return users.OrderByDescending(i => i.Name).ToList();
        }

        // Sorted and reversed users list by name.
        public List<User> GetReversedUsers()
        {
            return users.OrderBy(i => i.Name).Reverse().ToList();
        }

        // Paging.      
        public List<User> GetUsersPage(int pageSize, int pageIndex)
        {
            return users.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }
    }
}
