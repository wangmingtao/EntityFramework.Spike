using System.Data.Entity;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
            Database.Connection.ConnectionString =
                @"Server=(localDB)\MogulServicesDB; DataBase=AuthService; Integrated Security=true";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasMany(f=>f.SubSessions).WithRequired(f=>f.Session).HasForeignKey(f=>f.TokenId);
            //TODO:Add necessary mapping here.
        }
    }
}
