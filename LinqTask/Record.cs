using System;
using System.Collections.Generic;
using System.Text;

namespace LinqTask
{
    class Record
    {
        public User Author { get; set; }
        public String Message { get; set; }
        public Record(User author, String message)
        {
            this.Author = author;
            this.Message = message;
        }
        public override string ToString()
        {
            return string.Format("User: {0}; Message: {1}", Author, Message);
        }
    }
}
