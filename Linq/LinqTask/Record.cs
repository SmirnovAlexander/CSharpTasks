namespace LinqTask
{
    /// <summary>
    /// Class to store information about messages.
    /// </summary>
    public class Record
    {
        public User Author { get; set; }
        public string Message { get; set; }

        public Record(User author, string message)
        {
            Author = author;
            Message = message;
        }

        public override string ToString()
        {
            return string.Format("User: {0}; Message: {1}", Author, Message);
        }
    }
}
