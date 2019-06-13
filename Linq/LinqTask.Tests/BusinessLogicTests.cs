using System.Collections.Generic;
using NUnit.Framework;

namespace LinqTask.Tests
{
    [TestFixture]
    class BusinessLogicTests
    {
        List<User> users;
        List<Record> records;
        BusinessLogic businessLogic;
        List<User> listToCompare;

        [SetUp]
        protected void SetUp()
        {
            users = new List<User>();
            records = new List<Record>();
            listToCompare = new List<User>();

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

            businessLogic = new BusinessLogic(users, records);
        }

        [Test]
        public void GetUsersBySurname()
        {
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));
            listToCompare.Add(new User(6, "Lut", "Smirnov"));
            Assert.AreEqual(businessLogic.GetUsersBySurname("Smirnov"), listToCompare);
        }

        [Test]
        public void GetUsersBySurname2()
        {
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            Assert.AreNotEqual(businessLogic.GetUsersBySurname("Litvinov"), listToCompare);
        }

        [Test]
        public void GetUserByID()
        {
             Assert.AreEqual(businessLogic.GetUserByID(1), new User(1, "Alexander", "Smirnov"));
        }

        [Test]
        public void GetUserByID2()
        {
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            Assert.AreNotEqual(businessLogic.GetUserByID(3), new User(7, "Feodor", "Kupriyanov"));
        }

        [Test]
        public void GetUsersBySubstring()
        {
            listToCompare.Add(new User(3, "Vyacheslav", "Borodkin"));
            listToCompare.Add(new User(4, "Feodor", "Zhilkin"));
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            Assert.AreEqual(businessLogic.GetUsersBySubstring("od"), listToCompare);
        }

        [Test]
        public void GetUsersBySubstring2()
        {
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));
            listToCompare.Add(new User(2, "Mark", "Philippov"));
            listToCompare.Add(new User(5, "Stepan", "Litvinov"));
            listToCompare.Add(new User(6, "Lut", "Smirnov"));
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            Assert.AreEqual(businessLogic.GetUsersBySubstring("ov"), listToCompare);
        }

        [Test]
        public void GetAllUniqueNames()
        {
            List<string> listToCompare = new List<string>();
            listToCompare.Add("Alexander");
            listToCompare.Add("Mark");
            listToCompare.Add("Vyacheslav");
            listToCompare.Add("Feodor");
            listToCompare.Add("Stepan");
            listToCompare.Add("Lut");
            Assert.AreEqual(businessLogic.GetAllUniqueNames(), listToCompare);
        }

        [Test]
        public void GetAllAuthors()
        {
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));
            listToCompare.Add(new User(2, "Mark", "Philippov"));
            listToCompare.Add(new User(3, "Vyacheslav", "Borodkin"));
            listToCompare.Add(new User(4, "Feodor", "Zhilkin"));
            listToCompare.Add(new User(5, "Stepan", "Litvinov"));
            Assert.AreEqual(businessLogic.GetAllAuthors(), listToCompare);
        }

        [Test]
        public void GetMaxID()
        {
            Assert.AreEqual(businessLogic.GetMaxID(), 7);
        }

        [Test]
        public void GetOrderedUsers()
        {
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));
            listToCompare.Add(new User(4, "Feodor", "Zhilkin"));
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            listToCompare.Add(new User(6, "Lut", "Smirnov"));
            listToCompare.Add(new User(2, "Mark", "Philippov"));
            listToCompare.Add(new User(5, "Stepan", "Litvinov"));
            listToCompare.Add(new User(3, "Vyacheslav", "Borodkin"));
            Assert.AreEqual(businessLogic.GetOrderedUsers(), listToCompare);
        }

        [Test]
        public void GetDescendingOrderedUsers()
        {
            listToCompare.Add(new User(3, "Vyacheslav", "Borodkin"));
            listToCompare.Add(new User(5, "Stepan", "Litvinov"));
            listToCompare.Add(new User(2, "Mark", "Philippov"));
            listToCompare.Add(new User(6, "Lut", "Smirnov"));
            listToCompare.Add(new User(4, "Feodor", "Zhilkin"));
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));            
            Assert.AreEqual(businessLogic.GetDescendingOrderedUsers(), listToCompare);
        }

        [Test]
        public void GetReversedUsers()
        {
            listToCompare.Add(new User(3, "Vyacheslav", "Borodkin"));
            listToCompare.Add(new User(5, "Stepan", "Litvinov"));
            listToCompare.Add(new User(2, "Mark", "Philippov"));
            listToCompare.Add(new User(6, "Lut", "Smirnov"));
            listToCompare.Add(new User(7, "Feodor", "Kupriyanov"));
            listToCompare.Add(new User(4, "Feodor", "Zhilkin"));
            listToCompare.Add(new User(1, "Alexander", "Smirnov"));
            Assert.AreEqual(businessLogic.GetReversedUsers(), listToCompare);
        }
    }
}
