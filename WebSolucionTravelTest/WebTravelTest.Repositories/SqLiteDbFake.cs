using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebTravelTest.Entities.DbContexts;

namespace WebTravelTest.Repositories
{
    public class SqLiteDbFake
    {
        private DbContextOptions<ApplicationDbContext> options;
        public SqLiteDbFake()
        {
            options = GetDbContextOptions;
        }
        public ApplicationDbContext GetDbContext()
        {
            var context = new ApplicationDbContext(options);
            // Crea y abre el 'schema' en la base de datos
            context.Database.EnsureCreated();
            return context;
        }
        private DbContextOptions<ApplicationDbContext> GetDbContextOptions
        {
            get
            {
                // La BD in-memory solo existe cuando la conexión está abierta
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlite(connection)
                        .Options;

                return options;
            }
        }
    }
}
