using System;

namespace A10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string choice;
            do
            {
                Console.WriteLine("\n1) Display all Blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Display Posts");
                Console.WriteLine("4) Add Post");
                Console.Write("Please enter your selection or q to quit: ");
                choice = Console.ReadLine();

                DataManager data = new DataManager();
                switch (choice)
                {
                    case "1":
                        data.DisplayBlogs();
                        break;
                    case "2":
                        data.AddBlog();
                        break;
                    case "3":
                        data.DisplayPosts();
                        break;
                    case "4":
                        data.AddPost();
                        break;
                    default:
                        Console.WriteLine("Please select 1 to 4 or q to quit.");
                        break;
                }
            } while (choice != "q");
        }
    }
}
