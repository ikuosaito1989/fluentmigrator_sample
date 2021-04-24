using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace fluentmigrator_sample
{
    public static class Program
    {
        public const string connectionString = "";

        public static void Main(string[] args)
        {
            var parameters = Helpers.GetParams(args);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("connectionString is not found.");
            }

            var serviceProvider = CreateServices();

            using var scope = serviceProvider.CreateScope();

            if (parameters.DownVersion is null)
            {
                Up(scope.ServiceProvider);
            }
            else
            {
                Down(scope.ServiceProvider, parameters.DownVersion.Value);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void Up(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private static void Down(IServiceProvider serviceProvider, long version)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateDown(version);
        }
    }
}
