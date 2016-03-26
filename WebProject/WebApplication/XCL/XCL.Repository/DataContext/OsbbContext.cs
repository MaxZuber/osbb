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
        public DbSet<BuildingInfo> BuildingInfos { get; set; }
        public DbSet<Entrance> Entrances{ get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorValues> SensorValueses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(x => x.Id);
            modelBuilder.Entity<Account>().Property(x => x.Email).HasMaxLength(50);
            modelBuilder.Entity<Account>().Property(x => x.PhoneNumber).HasMaxLength(13);
            modelBuilder.Entity<Account>().Property(x => x.Password).HasMaxLength(50);
            modelBuilder.Entity<Account>().Property(x => x.VerificationToken).HasMaxLength(50);

            modelBuilder.Entity<BuildingInfo>().Property(x => x.StreetName).HasMaxLength(150);

            modelBuilder.Entity<BuildingInfo>()
                .HasMany(x => x.Entrances)
                .WithRequired(x => x.BuildingInfo)
                .HasForeignKey(x => x.BuildingId);

            modelBuilder.Entity<Sensor>().Property(x => x.Dimension).HasMaxLength(10);
            modelBuilder.Entity<Sensor>().Property(x => x.Description).HasMaxLength(1000);

            modelBuilder.Entity<Sensor>()
                .HasMany(x => x.SensorValues)
                .WithRequired(x => x.Sensor)
                .HasForeignKey(x => x.SensorId);

            modelBuilder.Entity<Entrance>()
                .HasMany(x => x.Flats)
                .WithRequired(x => x.Entrance)
                .HasForeignKey(x => x.EntranceId);

            modelBuilder.Entity<Entrance>()
                .HasMany(x => x.Sensors)
                .WithRequired(x => x.Entrance)
                .HasForeignKey(x => x.EntranceId);

            modelBuilder.Entity<Flat>()
                .HasMany(x => x.Accounts)
                .WithRequired(x => x.Flat)
                .HasForeignKey(x => x.FlatId);

            modelBuilder.Entity<SensorValues>().HasKey(x => x.DateTime);

            base.OnModelCreating(modelBuilder);
        }
    }
}
