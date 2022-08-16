using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace PhoneBookConsole
{
    public class cnBlogging : DbContext
    {
        /// <summary>
        /// Blog tabla
        /// </summary>
        public virtual DbSet<Blog> Blogs { get; set; }
        /// <summary>
        /// Post tabla
        /// </summary>
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database = Blog; User Id = sa; Password = reallyStrongPwd123");
            base.OnConfiguring(optionsBuilder);
        }

        private static string GetConnectionString(string connectionStringName)
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: true, reloadOnChange: false);
            var configuration = configurationBuilder.Build();
            return configuration.GetConnectionString(connectionStringName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity <Post>().ToTable(@"POSTS");
            modelBuilder.Entity <Post>().Property(
            x => x.PostId).HasColumnName(@"PostId").IsRequired().ValueGeneratedOnAdd(); //identity insert
            modelBuilder.Entity <Post>().HasKey(x => x.PostId);//primarykey
            modelBuilder.Entity <Post>().Property(
            x => x.Title).HasColumnName(@"Title").IsRequired().ValueGeneratedNever(); //not null
            modelBuilder.Entity<Post>().Property(
            x => x.Content).HasColumnName(@"Content").IsRequired().ValueGeneratedNever(); //not null

            modelBuilder.Entity <Blog>().ToTable(@"BLOGS");
            modelBuilder.Entity <Blog>().Property(
            x => x.BlogId).HasColumnName(@"BlogId").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Blog>().HasKey(x => x.BlogId);
            modelBuilder.Entity < Blog> ().Property(x => x.Name).HasColumnName(@"Name").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity < Blog>().HasMany(x => x.Posts);
        }
    }
}

