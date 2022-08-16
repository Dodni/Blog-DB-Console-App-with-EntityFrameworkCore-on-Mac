using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PhoneBookConsole;
using System.Reflection.Metadata;

namespace PhoneBookConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            AddBlog();
            BlogList();
            AddPost();
            AddPost();
            PostList();
        }

        private static void AddBlog()
        {
            using var db = new cnBlogging();
            db.Database.EnsureCreated();

            Console.WriteLine("Adja meg a blog nevet:");

            var name = Console.ReadLine();
            var blog = new Blog { Name = name };
            db.Blogs.Add(blog);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " //// "+ ex.InnerException.ToString());
                Console.ReadLine();
            }

        }

        static void BlogList()
        {
            using var db = new cnBlogging();
            try
            {
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;
                if (query.Any())
                {
                    Console.WriteLine("Minden blog az adatbázisban:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.BlogId + ". " + item.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Jelenleg nincs egyetlen blog sem elmentve");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.ReadLine();
            }
        }

        static void AddPost()
        {
            using var db = new cnBlogging();
            Console.WriteLine("\nPost hozzaadás a bloghoz:");
            Console.WriteLine("Kérem a Blog ID");
            var s = Console.ReadLine();

            if (!int.TryParse(s, out int blogid)) return;

            var blog = db.Blogs.ToList().Find(b => b.BlogId == blogid);
            if (blog == null) return;

            Console.WriteLine("Cim:");
            var title = Console.ReadLine();
            if (title.Length < 1) return;
            Console.WriteLine("Tartalom: ");
            var content = Console.ReadLine();
            if (content.Length < 1) return;
            var post = new Post { Title = title, Content = content, Blog = blog };
            blog.Posts.Add(post);
            db.Posts.Add(post);
            db.SaveChanges();
        }

        static void PostList()
        {
            var db = new cnBlogging();
            Console.WriteLine("\nPostok listázása");
            Console.WriteLine("Listázandó blog ID: ");
            var s = Console.ReadLine() ;
            if (!int.TryParse(s, out int blogid)) return;
            var blog = db.Blogs.Include(b => b.Posts).ToList().Find(b => b.BlogId == blogid);
            if (blog == null) return;
            Console.WriteLine(blog.BlogId + ". " + blog.Name);
            foreach (var item in blog.Posts.ToList())
            {
                Console.WriteLine("Cim:" + item.Title);
                Console.WriteLine("Tartalom: "+ item.Content + "\n");
            }
            Console.ReadLine();
        }
    }
}