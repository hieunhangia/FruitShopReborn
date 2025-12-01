using Core.Entities.Users;
using Core.Entities.Users.Staffs;

namespace Repository;

using Microsoft.EntityFrameworkCore;

public class FruitShopRebornDbContext(DbContextOptions<FruitShopRebornDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShippingInformation> ShippingInformations { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StaffInformation> StaffInformations { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<SalesStaff> SalesStaffs { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.UseTptMappingStrategy();
            
            e.HasKey(u => u.Id);
            
            e.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            e.HasIndex(u => u.Email).IsUnique();
            
            e.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            
            e.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);
            
            e.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
            
            e.Property(u => u.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        });
        
        modelBuilder.Entity<ShippingInformation>(e =>
        {
            e.HasKey(s => s.Id);
            
            e.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(255);
            
            e.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .IsUnicode(false);
            
            e.Property(s => s.CommuneWardCode)
                .IsRequired()
                .HasMaxLength(6)
                .IsFixedLength()
                .IsUnicode(false);
            
            e.Property(s => s.DetailAddress)
                .IsRequired()
                .HasMaxLength(255);
            
            e.HasOne(s => s.Customer)
                .WithMany(c => c.ShippingInformations)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Staff>(e =>
        {
            e.UseTptMappingStrategy();

            e.Property(s => s.HireDate)
                .IsRequired();
            
            e.HasOne(s => s.StaffInformation)
                .WithOne()
                .HasForeignKey<Staff>(s => s.StaffInformationId);
        });

        modelBuilder.Entity<StaffInformation>(e =>
        {
            e.HasKey(s => s.Id);
            
            e.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(255);
            
            e.Property(s => s.IdentityNumber)
                .IsRequired()
                .HasMaxLength(12)
                .IsFixedLength()
                .IsUnicode(false);
            e.HasIndex(s => s.IdentityNumber).IsUnique();
            
            e.Property(s => s.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .IsUnicode(false);
            e.HasIndex(s => s.PhoneNumber).IsUnique();
            
            e.Property(s => s.CommuneWardCode)
                .IsRequired()
                .HasMaxLength(6)
                .IsFixedLength()
                .IsUnicode(false);
            
            e.Property(s => s.DetailAddress)
                .IsRequired()
                .HasMaxLength(255);
        });
    }
}