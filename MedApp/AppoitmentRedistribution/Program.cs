using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MedAppData;
using Microsoft.EntityFrameworkCore;

namespace AppoitmentRedistribution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<ScheduleRedistribution>(hostContext.Configuration.GetSection("ScheduleRedistribution"));
                    services.AddHostedService<Worker>();
                    services.AddTransient(_ => _.GetRequiredService<IOptions<ScheduleRedistribution>>().Value);
                });
    }
}
