using Core.Entities.AiChat;
using Core.Entities.Users;
using Core.Entities.Users.Staffs;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class FruitShopRebornDbContext(DbContextOptions<FruitShopRebornDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShippingInformation> ShippingInformations { get; set; }
    public DbSet<AiConversation> Conversations { get; set; }
    public DbSet<AiChatMessage> ChatMessages { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<SalesStaff> SalesStaffs { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(e =>
        {
            e.UseTptMappingStrategy();
            
            e.Property(u => u.Status)
                .HasConversion<string>();
        });
        
        modelBuilder.Entity<ShippingInformation>(e =>
        {
            e.Property(s => s.PhoneNumber)
                .IsFixedLength();
            
            e.Property(s => s.CommuneWardCode)
                .IsFixedLength();
        });

        modelBuilder.Entity<Staff>(e =>
        {
            e.UseTptMappingStrategy();
            
            e.Property(s => s.IdentityNumber)
                .IsFixedLength();
            
            e.Property(s => s.PhoneNumber)
                .IsFixedLength();

            e.Property(s => s.CommuneWardCode)
                .IsFixedLength();
        });
        
        modelBuilder.Entity<AiChatMessage>(e =>
        {
            e.Property(c => c.Role)
                .HasConversion<string>();
        });
    }
}