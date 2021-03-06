using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VisualRecognition.Services;
using WatsonServices.Extensions;

namespace VisualRecognition
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("vcap_services.json", optional: true) // optionally read VCAP_SERVICES info from json file
                .AddVcapServices(); // add values from VCAP_SERVICES environment variable if it exists

            Configuration = configBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add Watson services. See WatsonServices/WatsonExtensions.cs
            services.AddWatsonServices(Configuration);

            // register other services
            services.AddTransient<IFileEncoderService, Base64FileEncoderService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Home",
                    template: "{action}",
                    defaults: new { controller = "Home" })
                .MapRoute(
                    name: "Api",
                    template: "{controller=api}/{action}/{classifierId}"
                    )
                .MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}");
            });
        }

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}