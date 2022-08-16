using System;
namespace PhoneBookConsole
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public Blog Blog { get; set; }


    }
}

