using System.Data.Entity;
using XCL.Common;
using XCL.Models.DbModels;

namespace XCL.Repository.DataContext
{
    public class OsbbContext: DbContext
    {

        public OsbbContext() : base(AppSettings.GetInstance().ConnectionString)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().Property(x => x.Email).HasMaxLength(50);
            modelBuilder.Entity<Account>().Property(x => x.PhoneNumber).HasMaxLength(13);
            modelBuilder.Entity<Account>().Property(x => x.Password).HasMaxLength(50);
            modelBuilder.Entity<Account>().Property(x => x.VerificationToken).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
