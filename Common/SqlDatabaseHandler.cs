using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Persistence.Contexts;

namespace Common
{
    public static class SqlDatabaseHandler
    {
        private static readonly object createLock = new object();
        private static ApplicationDbContext masterDbContext;
        private static readonly string ConnectionString = TestDatabaseConfiguration.GetConnectionString();

        private static ILoggerFactory loggerFactory = new NullLoggerFactory();
        // private static IGetCurrentUsername currentUsername = new TestCurrentUserName();
        // private static IPersistenceConfiguration persistenceConfig = new TestPersistenceConfiguration();

        // public static string Create()
        // {
        //     lock (createLock)
        //     {
        //         if (masterDbContext == null) CreateMasterDbContext();
        //         var databaseName = CreateNewDatabase();
        //
        //         using var nzPayrollDbContext = BuildNzPayrollDbContext(databaseName);
        //         nzPayrollDbContext.Database.Migrate();
        //
        //         return databaseName;
        //     }
        // }
        //
        // public static string CreateForApiTests()
        // {
        //     lock (createLock)
        //     {
        //         if (masterDbContext == null) CreateMasterDbContext();
        //         var databaseName = CreateNewDatabase();
        //
        //         using var nzPayrollDbContext = BuildNzPayrollDbContext(databaseName);
        //
        //         return $"{ConnectionString};Database={databaseName}";
        //     }
        // }

        // public static ApplicationDbContext BuildNzPayrollDbContext(string databaseName)
        // {
        //     return new ApplicationDbContext();
        // }

        private static string BuildConnectionStringForDatabase(string databaseName)
        {
            return $"{ConnectionString};Database={databaseName}";
        }

        private static string CreateNewDatabase()
        {
            var databaseName = Guid.NewGuid().ToString();

            masterDbContext.Database.ExecuteSqlRaw($"CREATE DATABASE [{databaseName}]");
            return databaseName;
        }

        private static void CreateMasterDbContext()
        {
            // var dbContextOptions = new DbContextOptionsBuilder<NzPayrollDbContext>()
                // .UseSqlServer($"{ConnectionString};Database=master")
                // .ConfigureWarnings(x => x.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning))
                // .Options;

            // masterDbContext = new NzPayrollDbContext(
                // dbContextOptions,
                // currentUsername,
                // persistenceConfig,
                // loggerFactory);
        }
    }
}