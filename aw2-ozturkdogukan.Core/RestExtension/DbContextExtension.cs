using aw2_ozturkdogukan.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace aw2_ozturkdogukan.Core.RestExtension
{
    public static class DbContextExtension
    {
        public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            var dbType = Configuration.GetConnectionString("DbType");
            if (dbType == "SQL")
            {
                var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<Aw2DbContext>(opts =>
                opts.UseSqlServer(dbConfig));
            }
            else if (dbType == "PostgreSql")
            {
                var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<Aw2DbContext>(opts =>
                  opts.UseNpgsql(dbConfig));
            }

        }
    }
}
