using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A10.Models;
using Microsoft.EntityFrameworkCore;

namespace A10
{
    public class DataManager
    {
        public void AddPost()
        {
            int blogNum = 0;
            try
            {
                Console.WriteLine("Please choose which Blog to add posts: ");
                // display the blog list
                DisplayBlogs();
                blogNum = int.Parse(Console.ReadLine());

            }
            catch (Exception ex)
            {
                // catch invalid input
                Console.WriteLine("Invalid Blog Id entered.");
                return;
            }

            using (var context = new DataContext())
            {
                // blogId doesn't match record
                var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogNum);
                if (blog == null)
                {
                    Console.WriteLine("There are no Blogs saved with that Id.");
                    return;
                }

                Console.Write("Enter your Post Title: ");
                var postTitle = Console.ReadLine();
                if (postTitle == null || postTitle.Equals(""))
                {
                    Console.WriteLine("Post tile can't be null.");
                    return;
                }

                Console.WriteLine("Enter your Post Content: ");
                var postContent = Console.ReadLine();

                var post = new Post { Title = postTitle, Content = postContent, BlogId = blogNum };

                context.Posts.Add(post);
                context.SaveChanges();
            }
        }

        public void DisplayBlogs(bool displayAllPosts = false)
        {
            Console.WriteLine("**************************");
            using (var context = new DataContext())
            {
                if (!context.Blogs.Any())
                {
                    Console.WriteLine("There's no Blogs.");
                    return;
                }

                var postFrom = "";
                if (displayAllPosts)
                {
                    Console.WriteLine("0) Posts from all blogs");
                    postFrom = "Posts from";
                }
                var blogs = context.Blogs;
                foreach (var blog in blogs)
                {
                    Console.WriteLine($"{blog.BlogId}) {postFrom} {blog.Name}");
                }
            }
        }

        public void AddBlog()
        {
            Console.WriteLine("\nEnter your Blog Name: ");
            var name = Console.ReadLine();
            if (name == null || name.Equals(""))
            {
                Console.WriteLine("Name can't be null");
            }

            //Add new blog entry to database table Blog
            var blog = new Blog();
            blog.Name = name;

            using (var context = new DataContext())
            {
                context.Blogs.Add(blog);
                context.SaveChanges();
            }
        }

        public void DisplayPosts()
        {
            Console.WriteLine("\nPlease select the Blog's posts to view: ");
            DisplayBlogs(true);
            var choice = int.Parse(Console.ReadLine());

            using (var context = new DataContext())
            {

                if (choice == 0)
                {
                    DisplayAllPosts();
                    return;
                }
                var blog = context.Blogs.Include(x => x.Posts).FirstOrDefault(x => x.BlogId == choice);
                DisplayPostsWithCount(blog.Posts);
            }
        }

        public void DisplayAllPosts()
        {
            using (var context = new DataContext())
            {
                if (!context.Posts.Any())
                {
                    Console.WriteLine("There's no Posts.");
                    return;
                }
                DisplayPostsWithCount(context.Posts.Include(x => x.Blog).ToList());
            }
        }

        public void DisplayPostsWithCount(List<Post> posts)
        {
            var postCount = posts == null ? 0 : posts.Count;
            Console.WriteLine($"{postCount} post(s) returned");
            if (postCount == 0) return;

            foreach (var post in posts)
            {
                Console.WriteLine($"Blog: {post.Blog.Name} \nTitle: {post.Title} \nContent: {post.Content}");
            }
        }
    }
}
