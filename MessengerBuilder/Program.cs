using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace MessengerBuilder
{
    class Program
    {
        static DbContext context = null;
        static string db;
        static void Main(string[] args)
        {
            try
            {
                string env = "Local";
                if (args.Length > 0)
                {
                    env = args[0];
                }
                if (env == "Prod")
                {
                    db = args[1];
                }
                else
                {
                    // Add package Microsoft.Extensions.Configuration.Json for AddJsonFile
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
                    db = config[$"MessengerContext:{env}"];
                }
                Console.Title = $"Messenger Builder -{env}";
                Console.WriteLine(db);
                var builder = new DbContextOptionsBuilder<MessengerContext>();
                //Add package Microsoft.EntityFrameworkCore.SqlServer for UseSqlServer
                builder.UseSqlServer(db);
                context = new MessengerContext(builder.Options);
                RecreateDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                throw;
            }
        }
        internal static void CreateDB()
        {
            Console.WriteLine("Create Database");
            context.Database.EnsureCreated();
        }
        internal static void DeleteDB()
        {
            Console.WriteLine("Delete Database");
            context.Database.EnsureDeleted();
        }
        internal static void RecreateDatabase()
        {
            DeleteDB();
            CreateDB();
        }
    }
}
