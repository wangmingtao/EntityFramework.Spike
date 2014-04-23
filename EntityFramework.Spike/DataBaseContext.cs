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
            modelBuilder.Entity<Session>().ToTable("Session");
            //TODO:Add necesary mapping here.
        }
    }
}
