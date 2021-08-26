using System;
using System.Collections.Generic;
using System.IO;
using App.Configuration;
using App.Extensions;
using App.Models;
using App.Serializers;
using Bullseye;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BasicEmployee = App.Models.BasicSerialization.Employee;
using CustomEmployee = App.Models.CustomSerialization.Employee;

namespace App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                var targets = new Targets();
                var basicSerializer = host.Services.GetRequiredService<IMemoryJsonSerializer<BasicEmployee>>();
                var customSerializer = host.Services.GetRequiredService<IMemoryJsonSerializer<CustomEmployee>>();
                targets.Add(TargetTypes.BasicSerialization, () =>
                {
                    var employeeBefore = Factory.CreateEmployeeWithBasicSerialization();
                    var json = basicSerializer.Serialize(employeeBefore);
                    employeeBefore.Dump("Basic json serialization done");
                    var employeeAfter = basicSerializer.Deserialize(json);
                    employeeAfter.Dump("Basic json deserialization done");
                });
                targets.Add(TargetTypes.CustomSerialization, () =>
                {
                    var employeeBefore = Factory.CreateEmployeeWithCustomSerialization();
                    var json = customSerializer.Serialize(employeeBefore);
                    employeeBefore.Dump("Custom json serialization done");
                    var employeeAfter = customSerializer.Deserialize(json);
                    employeeAfter.Dump("Custom json deserialization done");
                });
                targets.Add(TargetTypes.Default, dependsOn: new List<string>
                {
                    TargetTypes.BasicSerialization,
                    TargetTypes.CustomSerialization,
                });
                targets.RunAndExit(args);
            }

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<Settings>(context.Configuration.GetSection(nameof(Settings)));
                    services.AddTransient(typeof(IFileJsonSerializer<>), typeof(FileJsonSerializer<>));
                    services.AddTransient(typeof(IMemoryJsonSerializer<>), typeof(MemoryJsonSerializer<>));
                })
                .ConfigureLogging((_, loggingBuilder) =>
                {
                    loggingBuilder.AddNonGenericLogger();
                })
                .UseConsoleLifetime();

        private static class TargetTypes
        {
            public const string Default = "Default";
            public const string BasicSerialization = "Basic";
            public const string CustomSerialization = "Custom";
        }
    }
}
