using Microsoft.Extensions.Configuration;
using System.Windows;
using SpejderApplikation.Repository;
using SpejderApplikation.ViewModel;
using SpejderApplikation.View;

namespace SpejderApplikation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var configuration = LoadConfiguration();


            string ConnectionString = configuration.GetConnectionString("DefaultConnection");
            Connection.Initialize(ConnectionString);

            var ScoutsProgram = new ScoutsProgramView();
            //CurrentUser.Initialize("Region Syd", regionRepo);
        }
        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }

}
