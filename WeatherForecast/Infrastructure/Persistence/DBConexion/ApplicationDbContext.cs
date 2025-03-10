using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WeatherForecast.Infrastructure.Persistence.AccessLogs.Entities;
using WeatherForecast.Infrastructure.Persistence.Members.Entities;

namespace WeatherForecast.Infrastructure.Persistence.DBConexion
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                    {
                        dbCreator.Create();
                    }
                    if (!dbCreator.HasTables())
                    {
                        dbCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<AccessLogEntity> AccessLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MemberEntity>(builder =>
            {
                builder.HasKey(m => m.MemberID);
                builder.Property(m => m.FullName)
                    .HasColumnName("FullName")
                    .IsRequired()
                    .HasMaxLength(100);
                builder.Property(m => m.Email)
                    .HasColumnName("Email")
                    .IsRequired();
                builder.Property(m => m.Phone)
                    .HasColumnName("Phone");
                builder.Property(m => m.JoinDate)
                    .HasColumnName("JoinDate")
                    .IsRequired();
                builder.HasMany(m => m.AccessLogs)
                    .WithOne(a => a.Member)
                    .HasForeignKey(a => a.MemberID)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<AccessLogEntity>(builder =>
            {
                builder.HasKey(a => a.AccessID);

                builder.Property(a => a.AccessDateTime)
                    .HasColumnName("AccessDateTime");

                builder.Property(a => a.AccessType)
                    .HasColumnName("AccessType");

                builder.Property(a => a.AccessStatus)
                    .HasColumnName("AccessStatus");

                builder.HasOne(a => a.Member)
                    .WithMany(m => m.AccessLogs)
                    .HasForeignKey(a => a.MemberID)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }

    }
}
