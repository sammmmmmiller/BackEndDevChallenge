using BackEndDevChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackEndDevChallenge
{
    public class CalculatorContext : DbContext
    {
        // This is the connection string to the database
        // Please change this to your own connection string for testing.
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MathDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public DbSet<MathProblem> MathProblems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
