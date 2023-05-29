using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Account> accounts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DatabaseContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql( "server=localhost;user=root;database=ImageRecognitionDatabase;password=root;port=3306", serverVersion);

            //optionsBuilder.UseMySQL( Configuration.dbConnectionString);
        }
    }
}
