using System;
using Microsoft.Extensions.DependencyInjection;
using TP1.Services;

namespace TP1
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Hello World!");
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<NodeService>();
        }
    }
}
