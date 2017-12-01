using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.IntegrationTests
{
    public abstract class BaseIntegrationTests
    {
        protected virtual bool UseSqlServer => false;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            DestroyDatabase();
            CreateDatabase();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            DestroyDatabase();
        }

        protected void RunOnDatabase(Action<DatabaseService> databaseAction)
        {
            if (UseSqlServer)
            {
                RunOnSqlServer(databaseAction);
            }
            else
            {
                RunOnMemory(databaseAction);
            }
        }

        private void RunOnMemory(Action<DatabaseService> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase("MedPortalTests")
                .Options;

            using (var context = new DatabaseService(options))
            {
                databaseAction(context);
            }
        }

        private void RunOnSqlServer(Action<DatabaseService> databaseAction)
        {
            var connection = @"Server = .\SQLEXPRESS; Database = MedPortalTests; Trusted_Connection = true;";

            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseSqlServer(connection)
                .Options;

            using (var context = new DatabaseService(options))
            {
                databaseAction(context);
            }
        }

        private void CreateDatabase()
        {
            RunOnDatabase(ctx => ctx.Database.EnsureCreated());
        }

        private void DestroyDatabase()
        {
            RunOnDatabase(ctx => ctx.Database.EnsureDeleted());
        }
    }
}
