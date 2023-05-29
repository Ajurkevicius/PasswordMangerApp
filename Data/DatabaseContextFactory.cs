using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseMySql("server=localhost;user=root;database=ImageRecognitionDatabase;password=root;port=3306", serverVersion);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
