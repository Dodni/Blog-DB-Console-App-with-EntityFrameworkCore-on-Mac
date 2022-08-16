using System;
namespace PhoneBookConsole
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        public List<Post> Posts { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
        }

        /* Ez egy navigation property aminek az a szerepe v arra hasznaljuk, 
            hogy rajta keresztul tudjuk a bejarast biztositani. 
            Olyan mint egy külso kulcs mezo SQL szerveren es ez a nav prop-ban
            lesz tarolva, hogy melyik kapcsolt tabla ertekek vannak meg idekotve.
        '*/

    }
}

