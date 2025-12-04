using Core.Entities.AiChat;
using Core.Entities.Users;
using Core.Entities.Users.Staffs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class FruitShopRebornDbContext(DbContextOptions<FruitShopRebornDbContext> options) : IdentityDbContext<User>(options)
{
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

        ConfigureUser(modelBuilder);
        ConfigureCustomer(modelBuilder);
        ConfigureShippingInformation(modelBuilder);
        ConfigureStaff(modelBuilder);
        ConfigureAiConversation(modelBuilder);
        ConfigureAiChatMessage(modelBuilder);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();
        entity.UseTptMappingStrategy();

        entity.Ignore(u => u.Role);

        entity.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(BussinessRuleConstant.UserStatusMaxLength)
            .IsUnicode(false)
            .IsRequired();

        entity.Property(u => u.CreatedAt).IsRequired();
        entity.Property(u => u.UpdatedAt).IsRequired();
    }

    private static void ConfigureCustomer(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Customer>();

        entity.HasOne(c => c.DefaultShippingInformation)
            .WithMany()
            .HasForeignKey(c => c.DefaultShippingInformationId)
            .OnDelete(DeleteBehavior.NoAction);

        entity.HasMany(c => c.ShippingInformations)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(c => c.Conversations)
            .WithOne(conversation => conversation.Customer)
            .HasForeignKey(conversation => conversation.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureShippingInformation(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ShippingInformation>();
        entity.HasKey(s => s.Id);

        entity.Property(s => s.FullName)
            .HasMaxLength(BussinessRuleConstant.FullNameMaxLength)
            .IsRequired();

        entity.Property(s => s.PhoneNumber)
            .HasMaxLength(BussinessRuleConstant.PhoneNumberLength)
            .IsUnicode(false)
            .IsFixedLength()
            .IsRequired();

        entity.Property(s => s.CommuneWardCode)
            .HasMaxLength(BussinessRuleConstant.CommuneWardCodeLength)
            .IsUnicode(false)
            .IsFixedLength()
            .IsRequired();

        entity.Property(s => s.DetailAddress)
            .HasMaxLength(BussinessRuleConstant.DetailAddressMaxLength)
            .IsRequired();

        entity.HasOne(s => s.Customer)
            .WithMany(c => c.ShippingInformations)
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureStaff(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Staff>();
        entity.UseTptMappingStrategy();
        entity.HasIndex(s => s.IdentityNumber).IsUnique();
        entity.HasIndex(s => s.PhoneNumber).IsUnique();

        entity.Property(s => s.FullName)
            .HasMaxLength(BussinessRuleConstant.FullNameMaxLength)
            .IsRequired();

        entity.Property(s => s.IdentityNumber)
            .HasMaxLength(BussinessRuleConstant.IdentityNumberMaxLength)
            .IsUnicode(false)
            .IsFixedLength()
            .IsRequired();

        entity.Property(s => s.CommuneWardCode)
            .HasMaxLength(BussinessRuleConstant.CommuneWardCodeLength)
            .IsUnicode(false)
            .IsFixedLength()
            .IsRequired();

        entity.Property(s => s.DetailAddress)
            .HasMaxLength(BussinessRuleConstant.DetailAddressMaxLength)
            .IsRequired();

        entity.Property(s => s.HireDate)
            .IsRequired();
    }

    private static void ConfigureAiConversation(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<AiConversation>();
        entity.HasKey(c => c.Id);
        entity.Property(c => c.CreatedAt).IsRequired();

        entity.HasOne(c => c.Customer)
            .WithMany(customer => customer.Conversations)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureAiChatMessage(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<AiChatMessage>();
        entity.HasKey(m => m.Id);

        entity.Property(m => m.Role)
            .HasConversion<string>()
            .HasMaxLength(BussinessRuleConstant.AiChatMessageRoleMaxLength)
            .IsUnicode(false)
            .IsRequired();

        entity.Property(m => m.Content)
            .HasMaxLength(BussinessRuleConstant.AiChatMessageContentMaxLength)
            .IsRequired();

        entity.Property(m => m.CreatedAt).IsRequired();

        entity.HasOne(m => m.ChatHistory)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatHistoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}