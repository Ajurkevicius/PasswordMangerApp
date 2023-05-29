using ImageRecognition.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace ImageRecognition
{
   
    public partial class App : Application
    {

        private void ConfigureServices(IServiceCollection services)
        {
            
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql("server=localhost;user=root;database=ImageRecognitionDatabase;password=root;port=3306", serverVersion));

            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            
            var services = new ServiceCollection();
            ConfigureServices(services);

            
            var serviceProvider = services.BuildServiceProvider();

           
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DatabaseContext>();
                db.Database.Migrate();
            }

          
        }





       
    }
}
